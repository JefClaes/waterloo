using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace Waterloo.Bingoal
{
    public class BingoalFeed
    {
        public static RootObject Fetch(string url)
        {
            var webRequest = WebRequest.Create(url);

            using (var sr = new StreamReader(((HttpWebResponse)webRequest.GetResponse()).GetResponseStream()))
            {
                return JsonConvert.DeserializeObject<RootObject>(sr.ReadToEnd());
            }
        }

        public static RootObject Convert(string value)
        {
            return JsonConvert.DeserializeObject<RootObject>(value);
        }
    }
}
