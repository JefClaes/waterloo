using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waterloo.Bingoal
{
    public class SetScore
    {
        public string name { get; set; }
        public string p1 { get; set; }
        public string p2 { get; set; }
    }

    public class GameScore
    {
        public string p1 { get; set; }
        public string p2 { get; set; }
    }

    public class Option
    {
        public string quot { get; set; }
        public bool active { get; set; }
        public string id { get; set; }
        public string sov { get; set; }
        public string name { get; set; }
        public int dir { get; set; }
    }

    public class Subbet
    {
        public string id { get; set; }
        public int bettype { get; set; }
        public IList<Option> options { get; set; }
        public string match { get; set; }
        public string title { get; set; }
        public bool active { get; set; }
        public int b { get; set; }
    }

    public class Bet
    {
        public string id { get; set; }
        public string time { get; set; }
        public string flag { get; set; }
        public string name { get; set; }
        public string division { get; set; }
        public string score { get; set; }
        public string score1 { get; set; }
        public string score2 { get; set; }
        public bool tiebreak { get; set; }
        public int server { get; set; }
        public IList<SetScore> setScore { get; set; }
        public IList<GameScore> gameScore { get; set; }
        public string status { get; set; }
        public bool started { get; set; }
        public bool running { get; set; }
        public IList<Subbet> subbets { get; set; }
        public int subamount { get; set; }
    }

    public class Sport
    {
        public string name { get; set; }
        public string sport { get; set; }
        public IList<Bet> bets { get; set; }
    }

    public class RootObject
    {
        public IList<Sport> sports { get; set; }
        public int betamount { get; set; }
    }
}
