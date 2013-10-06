using System;
using System.IO;
using System.Linq;
using System.Security;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Text;

namespace bitnetly
{
    class Bitnetly : IBitService
    {
        public string Login { get; private set; }

        public string ApiKey { get; private set; }

        public string ApiVersion { get { return "2.0.1"; } }

        private string ApiRestUrl { get { return "http://api.bit.ly"; } }

        private Bitnetly()
        {
        }

        public Bitnetly(string login, string apikey)
        {
            if (login == null)
                throw new ArgumentNullException("login");

            if (apikey == null)
                throw new ArgumentNullException("apikey");

            Login = login;
            ApiKey = apikey;
        }

        public StatusCode Shorten(string url, out string shortened)
        {
            StatusCode statusCode;
            var shortUrls = Shorten(new string[] { url }, out statusCode);
            if (statusCode == StatusCode.OK && shortUrls.Count() == 1)
                shortened = shortUrls.First().ShortUrl;
            else
                shortened = string.Empty;
            return statusCode;
        }

        public string Shorten(string url)
        {
            string shortened;

            if (Shorten(url, out shortened) == StatusCode.OK)
                return shortened;

            return null;
        }

        private string GetShortenUrl(string[] longUrls)
        {
            StringBuilder urlBuilder = new StringBuilder();
            urlBuilder.Append(ApiRestUrl);
            urlBuilder.Append("/shorten?");
            urlBuilder.Append("login=");
            urlBuilder.Append(HttpUtility.UrlEncode(Login));
            urlBuilder.Append("&apiKey=");
            urlBuilder.Append(HttpUtility.UrlEncode(ApiKey));
            urlBuilder.Append("&version=");
            urlBuilder.Append(HttpUtility.UrlEncode(ApiVersion));
            urlBuilder.Append("&format=json&history=1");

            foreach (var url in longUrls)
            {
                urlBuilder.Append("&longUrl=");
                urlBuilder.Append(HttpUtility.UrlEncode(url));
            }

            string method = urlBuilder.ToString();
            return method;
        }

        public IBitResponse[] Shorten(string[] longUrls, out StatusCode statusCode)
        {
            if (longUrls == null)
                throw new ArgumentNullException("url");

            if (longUrls.Any(url => !Uri.IsWellFormedUriString(url, UriKind.Absolute)))
                throw new ArgumentException("Invalid absolute URL", "url");

            string method = GetShortenUrl(longUrls);

            try
            {

            }
            catch (SecurityException e)
            {
                throw new BitNetException(Reason.CallForbidden, "The local reader does not have sufficient permissions to access the location of the data.", e);
            }
            catch (FileNotFoundException e)
            {
                throw new BitNetException(Reason.MethodNotFound, "The file identified by the url does not exist.", e);
            }

            statusCode = StatusCode.UnknownError;
            return new IBitResponse[1];
        }
    }
}
