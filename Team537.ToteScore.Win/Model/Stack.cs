using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Team537.ToteScore.Win.Model
{
    public class Stack : BindableObject
    {
        bool can;
        bool litter;
        int height;

        private ICommand _setHeightCommand;
        public ICommand SetHeightCommand
        {
            get
            {
                return _setHeightCommand ?? (_setHeightCommand = new CommandHandler((height) => { SetHeight(Convert.ToInt32(height)); }, true));
            }
        }

        public void SetHeight(int height)
        {
            Height = height;
            if (height == 0)
            {
                Can = false;
                Litter = false;
            }
        }
        
        public bool Can
        {
            get { return can; }
            set
            {
                if (value != can)
                {
                    can = value;
                    this.NotifyPropertyChanged();
                    if (!value)
                    {
                        Litter = false;
                    }
                    this.NotifyPropertyChanged("Score");
                }
            }
        }

        public bool Litter
        {
            get { return litter; }
            set
            {
                if (value != litter)
                {
                    litter = value;
                    this.NotifyPropertyChanged();
                    if (value)
                    {
                        Can = true;
                    }
                    this.NotifyPropertyChanged("Score");
                }
            }
        }

        public int Height
        {
            get { return height; }
            set
            {
                if (value != height)
                {
                    height = value;
                    this.NotifyPropertyChanged();
                    this.NotifyPropertyChanged("Score");
                    this.NotifyPropertyChanged("Height0");
                    this.NotifyPropertyChanged("Height1");
                    this.NotifyPropertyChanged("Height2");
                    this.NotifyPropertyChanged("Height3");
                    this.NotifyPropertyChanged("Height4");
                    this.NotifyPropertyChanged("Height5");
                    this.NotifyPropertyChanged("Height6");
                }
            }
        }

        public bool Height0
        {
            get { return height >= 0; }
            set
            {
                SetHeight(0);
            }
        }

        public bool Height1
        {
            get { return height >= 1; }
            set
            {
                SetHeight(1);
            }
        }

        public bool Height2
        {
            get { return height >= 2; }
            set
            {
                SetHeight(2);
            }
        }

        public bool Height3
        {
            get { return height >= 3; }
            set
            {
                SetHeight(3);
            }
        }

        public bool Height4
        {
            get { return height >= 4; }
            set
            {
                SetHeight(4);
            }
        }

        public bool Height5
        {
            get { return height >= 5; }
            set
            {
                SetHeight(5);
            }
        }

        public bool Height6
        {
            get { return height >= 6; }
            set
            {
                SetHeight(6);
            }
        }

        public int Score
        {
            get
            {
                var stackScore = height * 2;
                var canScore = can ? height * 4 : 0;
                var litterScore = litter ? 6 : 0;

                return stackScore + canScore + litterScore;
            }
        }
    }
}
