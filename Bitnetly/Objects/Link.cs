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
        private string error;

        private DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

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

        [JsonProperty("error")]
        public string Error
        {
            get { return error; }
            set
            {
                if (value != error)
                {
                    error = value;
                }
            }
        }

        [JsonObject(Id="info", MemberSerialization=MemberSerialization.OptIn)]
        public struct LinkInfo
        {
            [JsonProperty("short_url")]
            string ShortURL;

            [JsonProperty("hash")]
            string Hash;

            [JsonProperty("user_hash")]
            string UserHash;

            [JsonProperty("global_hash")]
            string GlobalHash;

            [JsonProperty("title")]
            string Title;

            [JsonProperty("created_by")]
            string CreatedBy;

            [JsonProperty("created_at")]
            double CreatedAt;

            public DateTime GetCreatedTime()
            {
                DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return epoch.AddSeconds(CreatedAt);
            }
        }

        public struct SavedLink
        {
            string Link;
            string AggregateLink;
            string LongUrl;
        }

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static IList<Link> ExpandURL(string hash, string accessToken)
        {
            RestRequest r = new RestRequest("/expand", Method.GET);
            r.AddParameter("access_token", accessToken);
            r.AddParameter("hash", HttpUtility.UrlEncode(hash));
            RestClient c = new RestClient(BitNetlyService.APIURL + BitNetlyService.VERSION);

            IRestResponse i = c.Execute(r);

            BitNetlyException b = JsonConvert.DeserializeObject<BitNetlyException>(i.Content);

            if (i.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw b;
            }
            JToken j = JObject.Parse(JObject.Parse(i.Content).GetValue("data").ToString()).GetValue("expand");

            IList<Link> l = JsonConvert.DeserializeObject<IList<Link>>(j.ToString());

            return l;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shortURL"></param>
        /// <param name="accessToken"></param>
        public static IList<LinkInfo> Info(string shortURL, string accessToken)
        {
            RestClient c = new RestClient(BitNetlyService.APIURL + BitNetlyService.VERSION);
            RestRequest r = new RestRequest("/info", Method.GET);

            r.AddParameter("shortUrl", shortURL);
            r.AddParameter("access_token", accessToken);

            IRestResponse i = c.Execute(r);

            if (i.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new HttpException((int)i.StatusCode, i.StatusDescription);
            }

            JToken j = JObject.Parse(JObject.Parse(i.Content).GetValue("data").ToString()).GetValue("info");

            IList<LinkInfo> l = JsonConvert.DeserializeObject<IList<LinkInfo>>(j.ToString());

            return l;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shortURL"></param>
        /// <param name="accessToken"></param>
        /// <param name="title"></param>
        /// <param name="note"></param>
        public static void Edit(string shortURL, string accessToken, string title = "", string note = "")
        {
            RestRequest r = new RestRequest("/user/link_edit", Method.GET);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="longURL"></param>
        /// <param name="accessToken"></param>
        public static void Lookup(string[] longURL, string accessToken)
        {
            RestRequest r = new RestRequest("/user/link_lookup");

            foreach (string url in longURL)
            {
                r.AddParameter("url", HttpUtility.UrlEncode(url));
            }

            r.AddParameter("access_token", accessToken);

            RestClient c = new RestClient(BitNetlyService.APIURL + BitNetlyService.VERSION);

            IRestResponse i = c.Execute(r);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="longURL"></param>
        /// <param name="accessToken"></param>
        /// <param name="title"></param>
        /// <param name="note"></param>
        public static SavedLink Save(string longURL, string accessToken, string title = "", string note = "")
        {
            RestClient c = new RestClient(BitNetlyService.APIURL + BitNetlyService.VERSION);
            RestRequest r = new RestRequest("/user/link_save");

            if (longURL.Contains(' '))
            {
                throw new ArgumentException("The URL to be shortened cannot contain any spaces.");
            }

            if (longURL.Contains('?'))
            {
                if (longURL[longURL.IndexOf('?') - 1] != '/')
                {
                    longURL = longURL.Insert(longURL.IndexOf('?') - 1, "/");
                }
            }

            r.AddParameter("longUrl", HttpUtility.UrlEncode(longURL));
            r.AddParameter("access_token", accessToken);

            if (title != "")
            {
                r.AddParameter("title", title);
            }
            if (note != "")
            {
                r.AddParameter("note", note);
            }

            IRestResponse i = c.Execute(r);

            BitNetlyException b = JsonConvert.DeserializeObject<BitNetlyException>(i.Content);

            if (b.StatusText != "OK")
            {
                throw new HttpException(Convert.ToInt32(b.Status), b.StatusText);
            }

            JObject j = JObject.Parse(i.Content);
            JToken t = j.GetValue("data");

            SavedLink l = JsonConvert.DeserializeObject<SavedLink>(t.ToString());

            return l;
        }
    }
}
