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
        public ObservableCollection<Stack> Stacks { get; private set; }

        public MainWindowViewModel()
        {
            Stacks = new ObservableCollection<Stack>();
        }

        public int TotalScore
        {
            get
            {
                return Stacks.Sum(s => s.Score);
            }
        }

        public void AddStack()
        {
            var stack = new Stack();
            stack.PropertyChanged += StackPropertyChanged;
            Stacks.Add(stack);
        }

        void StackPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.NotifyPropertyChanged("TotalScore");
        }
    }
}
