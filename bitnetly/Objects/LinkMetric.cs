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

        /// <summary>
        /// 
        /// </summary>
        public struct Clicks
        {
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("link_clicks")]
            long ClickCount;

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("unit_refernce_ts")]
            long TimeStamp;

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("tz_offset")]
            int TimezoneOffset;

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("unit")]
            string Unit;

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("units")]
            int units;
        };

        private DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shortURL"></param>
        /// <param name="accesstoken"></param>
        /// <param name="rollup"></param>
        /// <param name="unit"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
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
