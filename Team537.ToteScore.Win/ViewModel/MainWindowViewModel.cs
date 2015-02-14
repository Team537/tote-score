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

                var autoScore = 0;

                if (toteSet)
                {
                    autoScore += 6;
                }

                if (stackedToteSet)
                {
                    autoScore += 20;
                }

                if (robotSet)
                {
                    autoScore += 4;
                }

                if (containerSet)
                {
                    autoScore += 8;
                }

                return autoScore + stackScore + coopertitionScore;
            }
        }

        public void AddStack()
        {
            var stack = new Stack();
            stack.PropertyChanged += StackPropertyChanged;
            Stacks.Add(stack);
        }

        public void Reset()
        {
            Stacks.Clear();
            this.CoopertitionSet = false;
            this.CoopertitionStack = false;
        }

        void StackPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.NotifyPropertyChanged("TotalScore");
            this.NotifyPropertyChanged("CanChangeTeam");
        }

        private bool robotSet;
        public bool RobotSet
        {
            get { return robotSet; }
            set
            {
                if (value != robotSet)
                {
                    robotSet = value;
                    this.NotifyPropertyChanged();
                    this.NotifyPropertyChanged("TotalScore");
                }
            }
        }

        private bool toteSet;
        public bool ToteSet
        {
            get { return toteSet; }
            set
            {
                if (value != toteSet)
                {
                    toteSet = value;
                    if (value)
                    {
                        this.StackedToteSet = false;
                    }
                    this.NotifyPropertyChanged();
                    this.NotifyPropertyChanged("TotalScore");
                }
            }
        }

        private bool stackedToteSet;
        public bool StackedToteSet
        {
            get { return stackedToteSet; }
            set
            {
                if (value != stackedToteSet)
                {
                    stackedToteSet = value;
                    if (value)
                    {
                        this.ToteSet = false;
                    }
                    this.NotifyPropertyChanged();
                    this.NotifyPropertyChanged("TotalScore");
                }
            }
        }

        private bool containerSet;
        public bool ContainerSet
        {
            get { return containerSet; }
            set
            {
                if (value != containerSet)
                {
                    toteSet = value;
                    this.NotifyPropertyChanged();
                    this.NotifyPropertyChanged("TotalScore");
                }
            }
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
                    this.NotifyPropertyChanged("CanChangeTeam");
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
                    this.NotifyPropertyChanged("CanChangeTeam");
                }
            }
        }

        private bool isRed;
        public bool IsRed
        {
            get { return isRed; }
            set
            {
                if (value != isRed)
                {
                    isRed = value;
                    this.NotifyPropertyChanged();
                    this.NotifyPropertyChanged("IsBlue");
                    this.NotifyPropertyChanged("CurrentAlliance");
                }
            }
        }

        public bool IsBlue
        {
            get { return !this.IsRed; }
            set
            {
                this.IsRed = !value;
            }
        }

        public bool CanChangeTeam
        {
            get { return this.TotalScore == 0; }
        }

        public string CurrentAlliance
        {
            get
            {
                return isRed ? "Red" : "Blue";
            }
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
        
    }
}
