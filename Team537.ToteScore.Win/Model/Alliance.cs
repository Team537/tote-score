﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team537.ToteScore.Win.Model
{
    public class Alliance : BindableObject
    {
        public ObservableCollection<Stack> Stacks { get; private set; }

        public Alliance()
        {
            Stacks = new ObservableCollection<Stack>();
        }

        public void AddStack()
        {
            var stack = new Stack();
            stack.PropertyChanged += StackPropertyChanged;
            Stacks.Add(stack);
        }

        public void Reset()
        {
            this.UnprocessedLitter = 0;
            this.ScoredLitter = 0;
            Stacks.Clear();
            this.CoopertitionSet = false;
            this.CoopertitionStack = false;
            this.RobotSet = false;
            this.ToteSet = false;
            this.StackedToteSet = false;
            this.ContainerSet = false;
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
                    this.NotifyPropertyChanged("CanChangeTeam");
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
                    this.NotifyPropertyChanged("CanChangeTeam");
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
                    this.NotifyPropertyChanged("CanChangeTeam");
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
                    this.NotifyPropertyChanged("CanChangeTeam");
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

        private int unprocessedLitter;
        public int UnprocessedLitter
        {
            get { return unprocessedLitter; }
            set
            {
                if (value != unprocessedLitter)
                {
                    unprocessedLitter = value;
                    this.NotifyPropertyChanged();
                    this.NotifyPropertyChanged("TotalScore");
                }
            }
        }

        private int scoredLitter;
        public int ScoredLitter
        {
            get { return scoredLitter; }
            set
            {
                if (value != scoredLitter)
                {
                    scoredLitter = value;
                    this.NotifyPropertyChanged();
                    this.NotifyPropertyChanged("TotalScore");
                }
            }
        }

        // reporting scores
 
        public int CoopScore
        {
            get
            {
                return coopertitionStack ? 40 : coopertitionSet ? 20 : 0;
            }
        }

        public int AutoScore
        {
            get
            {
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
                return autoScore;
            }
        }

        public int CanScore
        {
            get
            {
                return Stacks.Sum(s => s.CanScore);
            }
        }

        public int ToteScore
        {
            get
            {
                return Stacks.Sum(s => s.ToteScore);
            }
        }

        public int LitterScore
        {
            get
            {
                var litterScore = scoredLitter + (unprocessedLitter * 4);
                var cannedLitter = Stacks.Sum(s => s.LitterScore);
                return litterScore + cannedLitter;
            }
        }

        public int TotalScore
        {
            get
            {
                return AutoScore + CanScore + CoopScore + LitterScore + ToteScore;
            }
        }
    }
}
