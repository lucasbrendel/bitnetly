using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using System.Web;
using BitNetly;
using Newtonsoft.Json.Linq;
using BitNetly.Exceptions;

namespace BitNetly.Objects
{
    public class Link
    {
        private string globalHash;
        private string hash;
        private string longURL;
        private string newHash;
        private string url;

        [JsonProperty("global_hash")]
        public string GlobalHash
        {
            get { return globalHash; }
            set
            {
                if (value != globalHash)
                {
                    globalHash = value;
                }
            }
        }

        [JsonProperty("hash")]
        public string Hash
        {
            get { return hash; }
            set
            {
                if (value != hash)
                {
                    hash = value;
                }
            }
        }

        [JsonProperty("long_url")]
        public string LongURL
        {
            get { return longURL; }
            set
            {
                if (value != longURL)
                {
                    longURL = value;
                }
            }
        }

        [JsonProperty("new_hash")]
        public string NewHash
        {
            get { return newHash; }
            set
            {
                if (value != newHash)
                {
                    newHash = value;
                }
            }
        }

        [JsonProperty("url")]
        public string URL
        {
            get { return url; }
            set
            {
                if (value != url)
                {
                    url = value;
                }
            }
        }

        public Link()
        {

        }

        public Link(string url)
        {

        }

        public static Link ExpandURL(string shortURL, string accessToken)
        {
            RestRequest r = new RestRequest("/expand", Method.GET);
            r.AddParameter("access_token", accessToken);
            r.AddParameter("shortUrl", HttpUtility.UrlEncode(shortURL));
            RestClient c = new RestClient(BitNetlyService.APIURL + BitNetlyService.VERSION);

            IRestResponse i = c.Execute(r);

            BitNetlyException b = JsonConvert.DeserializeObject<BitNetlyException>(i.Content);

            if (i.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw b;
            }

            Link l = JsonConvert.DeserializeObject<Link>(i.Content);

            return l;

        }

        public void Info()
        {

        }

        /// <summary>
        /// Shortens a given URL
        /// </summary>
        /// <param name="longURL">The URL to be shortened</param>
        /// <param name="accessToken">Generic access token to use bit.ly API</param>
        /// <exception cref="BitNetlyException">Error occurring during the request</exception>
        /// <returns>Shortened URL</returns>
        public static string ShortenURL(string longURL, string accessToken)
        {
            return Shorten(longURL, accessToken).URL;
        }

        private static Link Shorten(string longURL, string accessToken)
        {
            Link shortURL;
            string shortenMethod = "shorten";

            RestRequest request = new RestRequest(String.Format("/{0}?access_token={1}&longUrl={2}", shortenMethod, accessToken, HttpUtility.UrlEncode(longURL)), Method.GET);
            RestClient client = new RestClient(BitNetlyService.APIURL + BitNetlyService.VERSION);

            IRestResponse r = client.Execute(request);

            BitNetlyException b = JsonConvert.DeserializeObject<BitNetlyException>(r.Content);

            if (b.Status != "200")
            {
                throw b;
            }
            else
            {
                JObject j = JObject.Parse(r.Content);
                JToken t = j.GetValue("data");
                shortURL = JsonConvert.DeserializeObject<Link>(t.ToString());
            }

            return shortURL;
        }

        public void Shorten()
        {

        }

        public void Lookup()
        {

        }

        public void LinkEdit()
        {

        }

        public void LinkSave()
        {

        }
    }
}
