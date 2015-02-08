using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Waterloo.Napoleon
{
    public class NapoleonFeed
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
