using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Team537.ToteScore.Win
{
    /// <summary>
    /// Interaction logic for AllianceControl.xaml
    /// </summary>
    public partial class AllianceControl : UserControl
    {
        protected Alliance ViewModel
        {
            get { return this.DataContext as Alliance; }
        }

        public AllianceControl()
        {
            InitializeComponent();
        }
        
        private void AddStackButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddStack();
        }
    }
}
