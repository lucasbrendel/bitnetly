using RestSharp;
using System;
using System.Web;
using RestSharp.Authenticators;

namespace BitNetly
{
    public class BitNetlyService
    {
        private string accessToken;
        private string clientID;
        private string clientSecret;

        public enum LoginRoute
        {
            Basic,
            OAuth2
        };

        /// <summary>
        /// 
        /// </summary>
        public const string VERSION = "v3/";

        /// <summary>
        /// 
        /// </summary>
        public const string APIURL = "https://api-ssl.bitly.com/";

        public const string OAUTHURL = "https://bitly.com/";

        /// <summary>
        /// 
        /// </summary>
        public BitNetlyService(string accessToken)
        {
            this.accessToken = accessToken;
        }

        public BitNetlyService(string username, string password, string clientID, string clientSecret, LoginRoute route)
        {
            this.clientID = clientID;
            this.clientSecret = clientSecret;
            switch(route)
            {
                case LoginRoute.Basic:
                    this.accessToken = this.BasicAuthLogin(username, password);
                    break;
                case LoginRoute.OAuth2:
                    this.accessToken = this.OAuth2Login(username, password);
                    break;
            }
        }

        private string BasicAuthLogin(string username, string password)
        {
            string token = String.Empty;

            RestClient c = new RestClient(APIURL);
            c.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest r = new RestRequest("/oauth/access_token", Method.POST);
            //r.AddParameter("grant_type", "password");
            r.AddParameter("client_id", this.clientID);
            r.AddParameter("client_secret", this.clientSecret);
            r.AddParameter("Content-Type", "application/x-www-form-urlencoded");
            string keys = String.Format("{0}:{1}", username, password);
            r.AddHeader("Authorization", "Basic " + EncodeTo64(keys));
            RestResponse i = (RestResponse)c.Execute(r);

            if (i.StatusCode == System.Net.HttpStatusCode.OK)
            {
                token = i.Content;
            }

            return token;
        }

        private string OAuth2Login(string username, string password)
        {
            string token = String.Empty;

            RestClient c = new RestClient(OAUTHURL);
            RestRequest r = new RestRequest("oauth/authorize");

            return token;
        }

        private string EncodeTo64(string code)
        {
            byte[] toEncodeAsBytes
              = System.Text.ASCIIEncoding.ASCII.GetBytes(code);
            string returnValue
                  = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }
    }
}
