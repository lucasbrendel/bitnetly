using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using BitNetly;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace BitNetly.Objects
{
    public class LinkMetric
    {
        public enum TimeUnits
        {
            Minute,
            Hour,
            Day,
            Week,
            Month,
            All
        };

        public enum TimeZones
        {
        
        };

        public struct Clicks
        {
            [JsonProperty("link_clicks")]
            long ClickCount;

            [JsonProperty("unit_refernce_ts")]
            long TimeStamp;

            [JsonProperty("tz_offset")]
            int TimezoneOffset;

            [JsonProperty("unit")]
            string Unit;

            [JsonProperty("units")]
            int units;
        };

        private DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        public static Clicks GetClicks(string shortURL, string accesstoken, bool rollup = true, TimeUnits unit = TimeUnits.All, int limit = 100)
        {
            RestRequest r = new RestRequest("/link/clicks", Method.GET);
            RestClient c = new RestClient(BitNetlyService.APIURL + BitNetlyService.VERSION);

            r.AddParameter("access_token", accesstoken);
            r.AddParameter("link", shortURL);
            //r.AddParameter("rollup", rollup.ToString());
            r.AddParameter("limit", limit);

            IRestResponse i = c.Execute(r);

            BitNetly.Exceptions.BitNetlyException b = JsonConvert.DeserializeObject<BitNetly.Exceptions.BitNetlyException>(i.Content);

            if (i.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw b;
            }

            JObject t = JObject.Parse(i.Content);
            JToken j = t.GetValue("data");
            
            Clicks l = JsonConvert.DeserializeObject<Clicks>(j.ToString());

            return l;

        }
    }
}
