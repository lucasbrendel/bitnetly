
namespace BitNetly
{
    public class BitNetlyService
    {
        private string accessToken;

        /// <summary>
        /// 
        /// </summary>
        public const string VERSION = "v3/";

        /// <summary>
        /// 
        /// </summary>
        public const string APIURL = "https://api-ssl.bitly.com/";

        /// <summary>
        /// 
        /// </summary>
        public BitNetlyService(string accessToken)
        {
            this.accessToken = accessToken;
        }
    }
}
