using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waterloo.Napoleon
{
    public class Path
    {
        public int id { get; set; }
        public string name { get; set; }
        public string englishName { get; set; }
    }

    public class Event
    {
        public int id { get; set; }
        public string name { get; set; }
        public string homeName { get; set; }
        public string awayName { get; set; }
        public string start { get; set; }
        public string group { get; set; }
        public string type { get; set; }
        public bool liveBetOffers { get; set; }
        public bool openForLiveBetting { get; set; }
        public string boUri { get; set; }
        public int groupId { get; set; }
        public bool hideStartNo { get; set; }
        public int sportId { get; set; }
        public string sport { get; set; }
        public List<Path> path { get; set; }
        public string englishName { get; set; }
        public string state { get; set; }
    }

    public class MatchClock
    {
        public int minute { get; set; }
        public int second { get; set; }
        public string period { get; set; }
        public bool running { get; set; }
    }

    public class Score
    {
        public string home { get; set; }
        public string away { get; set; }
        public string info { get; set; }
    }

    public class Home
    {
        public int yellowCards { get; set; }
        public int redCards { get; set; }
        public int corners { get; set; }
    }

    public class Away
    {
        public int yellowCards { get; set; }
        public int redCards { get; set; }
        public int corners { get; set; }
    }

    public class Football
    {
        public Home home { get; set; }
        public Away away { get; set; }
    }

    public class Sets
    {
        public List<int> home { get; set; }
        public List<int> away { get; set; }
        public bool homeServe { get; set; }
    }

    public class Statistics
    {
        public Football football { get; set; }
        public Sets sets { get; set; }
    }

    public class LiveData
    {
        public int eventId { get; set; }
        public MatchClock matchClock { get; set; }
        public Score score { get; set; }
        public Statistics statistics { get; set; }
        public bool open { get; set; }
    }

    public class Criterion
    {
        public int id { get; set; }
        public string label { get; set; }
    }

    public class BetOfferType
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Pba
    {
        public bool disabled { get; set; }
        public string status { get; set; }
    }

    public class Outcome
    {
        public int id { get; set; }
        public string label { get; set; }
        public int odds { get; set; }
        public string type { get; set; }
        public int betOfferId { get; set; }
        public string changedDate { get; set; }
        public string oddsFractional { get; set; }
        public string oddsAmerican { get; set; }
    }

    public class MainBetOffer
    {
        public int id { get; set; }
        public bool live { get; set; }
        public Criterion criterion { get; set; }
        public BetOfferType betOfferType { get; set; }
        public int eventId { get; set; }
        public Pba pba { get; set; }
        public bool cashIn { get; set; }
        public List<Outcome> outcomes { get; set; }
        public bool main { get; set; }
        public string categoryName { get; set; }
        public bool? suspended { get; set; }
        public bool? open { get; set; }
    }

    public class LiveEvent
    {
        public Event @event { get; set; }
        public LiveData liveData { get; set; }
        public MainBetOffer mainBetOffer { get; set; }
    }

    public class Group4
    {
        public int id { get; set; }
        public string name { get; set; }
        public string englishName { get; set; }
        public string sport { get; set; }
    }

    public class Group3
    {
        public int id { get; set; }
        public string name { get; set; }
        public string englishName { get; set; }
        public List<Group4> groups { get; set; }
        public string sport { get; set; }
        public string sortOrder { get; set; }
    }

    public class Group2
    {
        public int id { get; set; }
        public string name { get; set; }
        public string englishName { get; set; }
        public List<Group3> groups { get; set; }
        public string sortOrder { get; set; }
        public string sport { get; set; }
    }

    public class Group
    {
        public int id { get; set; }
        public List<Group2> groups { get; set; }
        public string sport { get; set; }
    }

    public class RootObject
    {
        public List<LiveEvent> liveEvents { get; set; }
        public Group group { get; set; }
    }
}
