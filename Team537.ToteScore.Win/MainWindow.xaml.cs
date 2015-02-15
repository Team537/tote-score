using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Team537.ToteScore.Win.Model;
using Team537.ToteScore.Win.ViewModel;

namespace Team537.ToteScore.Win
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel viewModel;
        TcpClient client = null;
        StreamWriter writer = null;

        public MainWindow()
        {
            InitializeComponent();

            viewModel = new MainWindowViewModel();
            viewModel.Red.PropertyChanged += AlliancePropertyChanged;
            viewModel.Blue.PropertyChanged += AlliancePropertyChanged;

            viewModel.ConnectionAddress = "127.0.0.1";
            viewModel.CanConnect = true;
            viewModel.CanDisconnect = false;

            this.DataContext = viewModel;
        }

        void AlliancePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "TotalScore")
            {
                this.SendScore(true, viewModel.Red.TotalScore);
                this.SendScore(false, viewModel.Blue.TotalScore);
            }
        }

        private void SendScore(bool isRed, int score)
        {
            if (client != null && client.Connected && writer != null)
            {
                var command = String.Format("{0} {1}", isRed ? "rs" : "bs", score);

                try
                {
                    writer.WriteLine(command);
                    writer.Flush();
                    Debug.WriteLine("command sent");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    this.Disconnect();
                }
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            var messageBoxResult = MessageBox.Show("Are you sure you want to reset? Cannot be undone.", "", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                viewModel.Red.Reset();
                viewModel.Blue.Reset();
            }
        }

        private async void Connect_Click(object sender, RoutedEventArgs e)
        {
            var ipAddress = viewModel.ConnectionAddress;
            Debug.WriteLine(ipAddress);

            IPAddress remoteServer;
            if (!IPAddress.TryParse(ipAddress, out remoteServer))
            {
                MessageBox.Show("Invalid address");
            }

            try
            {
                client = new TcpClient();
                await client.ConnectAsync(remoteServer, 8005);
                writer = new StreamWriter(client.GetStream());

                viewModel.CanConnect = false;
                viewModel.CanDisconnect = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Debug.WriteLine(ex.Message);
            }

            this.SendScore(true, viewModel.Red.TotalScore);
            this.SendScore(false, viewModel.Blue.TotalScore);
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            this.Disconnect();
        }

        private void Disconnect()
        {
            try
            {
                client.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            writer = null;
            Debug.WriteLine("Disconnected");

            viewModel.CanConnect = true;
            viewModel.CanDisconnect = false;
        }

        private async void CommitButton_Click(object sender, RoutedEventArgs e)
        {
            var results = new MatchResults();

            results.MatchNumber = viewModel.MatchNumber;

            results.RedScore = viewModel.Red.TotalScore;
            results.RedCoopScore = viewModel.Red.CoopScore;
            results.RedAutoScore = viewModel.Red.AutoScore;
            results.RedCanScore = viewModel.Red.CanScore;
            results.RedToteScore = viewModel.Red.ToteScore;
            results.RedLitterScore = viewModel.Red.LitterScore;

            results.BlueScore = viewModel.Blue.TotalScore;
            results.BlueCoopScore = viewModel.Blue.CoopScore;
            results.BlueAutoScore = viewModel.Blue.AutoScore;
            results.BlueCanScore = viewModel.Blue.CanScore;
            results.BlueToteScore = viewModel.Blue.ToteScore;
            results.BlueLitterScore = viewModel.Blue.LitterScore;

            using(var httpClient = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(results);
                var requestContent = new StringContent(json);
                await httpClient.PostAsync(viewModel.ResultsAddress, requestContent);
            }
        }
    }
}
