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
                var stackScore = Stacks.Sum(s => s.Score);
                var coopertitionScore = coopertitionStack ? 40 : coopertitionSet ? 20 : 0;
                return stackScore + coopertitionScore;
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
        
        private bool coopertitionSet;
        public bool CoopertitionSet
        {
            get { return coopertitionSet; }
            set
            {
                if (value != coopertitionSet)
                {
                    coopertitionSet = value;
                    if (value)
                    {
                        CoopertitionStack = false;
                    }
                    this.NotifyPropertyChanged();
                    this.NotifyPropertyChanged("CoopertitionSet");
                    this.NotifyPropertyChanged("TotalScore");
                }
            }
        }

        private bool coopertitionStack;
        public bool CoopertitionStack
        {
            get { return coopertitionStack; }
            set
            {
                if (value != coopertitionStack)
                {
                    coopertitionStack = value;
                    if (value)
                    {
                        CoopertitionSet = false;
                    }
                    this.NotifyPropertyChanged();
                    this.NotifyPropertyChanged("CoopertitionStack");
                    this.NotifyPropertyChanged("TotalScore");
                }
            }
        }
    }
}
