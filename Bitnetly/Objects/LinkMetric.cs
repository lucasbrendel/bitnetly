using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RestSharp;

namespace BitNetly.Objects
{
    /// <summary>
    /// Allows requests that return metric information about links
    /// </summary>
    public class LinkMetric
    {
        /// <summary>
        /// Possible time units the user can request
        /// </summary>
        public enum TimeUnits
        {
            /// <summary>
            /// 
            /// </summary>
            Minute,

            /// <summary>
            /// 
            /// </summary>
            Hour,

            /// <summary>
            /// 
            /// </summary>
            Day,

            /// <summary>
            /// 
            /// </summary>
            Week,

            /// <summary>
            /// 
            /// </summary>
            Month,

            /// <summary>
            /// 
            /// </summary>
            All
        };

        public enum TimeZones
        {
        
        };

        /// <summary>
        /// 
        /// </summary>
        public struct LinkClicks
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

        /// <summary>
        /// 
        /// </summary>
        public struct CountryClick
        {
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("country")]
            string Country;

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("clicks")]
            string Clicks;
        };

        /// <summary>
        /// 
        /// </summary>
        public struct LinkCountries
        {
            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("tz_offset")]
            string TimeZoneOffset;

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("unit")]
            string Unit;

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("units")]
            string Units;

            /// <summary>
            /// 
            /// </summary>
            [JsonProperty("countries")]
            CountryClick[] Countries; 
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shortURL"></param>
        /// <param name="accesstoken"></param>
        /// <param name="rollup"></param>
        /// <param name="unit"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public static LinkClicks Clicks(string shortURL, string accesstoken, bool rollup = true, TimeUnits unit = TimeUnits.All, int limit = 100)
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
            
            LinkClicks l = JsonConvert.DeserializeObject<LinkClicks>(j.ToString());

            return l;
        }

        /// <summary>
        /// Requests the country information about a bit.ly link
        /// </summary>
        /// <param name="shortURL">The shortened URL of which to get metrics</param>
        /// <param name="accesstoken">The applications access token to the bit.ly API</param>
        /// <param name="unit">THe time unit to display the results</param>
        /// <param name="limit">The maximum amount of results to return</param>
        /// <returns></returns>
        public static LinkCountries Countries(string shortURL, string accesstoken, TimeUnits unit = TimeUnits.All, int limit = 100)
        {
            RestRequest r = new RestRequest("/link/countries", Method.GET);
            RestClient c = new RestClient(BitNetlyService.APIURL + BitNetlyService.VERSION);

            r.AddParameter("access_token", accesstoken);
            r.AddParameter("link", shortURL);
            r.AddParameter("limit", limit);

            IRestResponse i = c.Execute(r);

            BitNetly.Exceptions.BitNetlyException b = JsonConvert.DeserializeObject<BitNetly.Exceptions.BitNetlyException>(i.Content);

            if (i.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw b;
            }

            JObject t = JObject.Parse(i.Content);
            JToken j = t.GetValue("data");

            LinkCountries l = JsonConvert.DeserializeObject<LinkCountries>(j.ToString());

            return l;
        }
    }
}
