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
                }
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

        public void Clear()
        {
            Height = 0;
            Can = false;
            Litter = false;
        }
    }
}
