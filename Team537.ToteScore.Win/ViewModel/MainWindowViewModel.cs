using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team537.ToteScore.Win.Model;

namespace Team537.ToteScore.Win.ViewModel
{
    public class MainWindowViewModel : BindableObject
    {
        
        //public bool CanChangeTeam
        //{
        //    get { return this.TotalScore == 0; }
        //}

        private int matchNumber;
        public int MatchNumber
        {
            get { return matchNumber; }
            set
            {
                if (value != matchNumber)
                {
                    matchNumber = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public Alliance Red { get; private set; }

        public Alliance Blue { get; private set; }

        public MainWindowViewModel()
        {
            Red = new Alliance();
            Blue = new Alliance();
        }        

        private bool canConnect;
        public bool CanConnect
        {
            get { return canConnect; }
            set
            {
                if (value != canConnect)
                {
                    canConnect = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        private bool canDisconnect;
        public bool CanDisconnect
        {
            get { return canDisconnect; }
            set
            {
                if (value != canDisconnect)
                {
                    canDisconnect = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        private string connectionAddress;
        public string ConnectionAddress
        {
            get { return connectionAddress; }
            set
            {
                if (value != connectionAddress)
                {
                    connectionAddress = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        private string resultsAddress;
        public string ResultsAddress
        {
            get { return resultsAddress; }
            set
            {
                if (value != resultsAddress)
                {
                    resultsAddress = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
    }
}
