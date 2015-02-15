using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team537.ToteScore.Win.Model
{
    public class MatchResults
    {
        public int MatchNumber { get; set; }

        public int RedScore { get; set; }
        public int RedCoopScore { get; set; }
        public int RedAutoScore { get; set; }
        public int RedCanScore { get; set; }
        public int RedToteScore { get; set; }
        public int RedLitterScore { get; set; }

        public int BlueScore { get; set; }
        public int BlueCoopScore { get; set; }
        public int BlueAutoScore { get; set; }
        public int BlueCanScore { get; set; }
        public int BlueToteScore { get; set; }
        public int BlueLitterScore { get; set; }
    }
}
