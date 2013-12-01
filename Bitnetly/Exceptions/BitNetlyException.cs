using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;

namespace BitNetly.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class BitNetlyException : Exception
    {
        private string status;

        private string text;

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("status_code")]
        public string Status
        {
            get { return status; }
        }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("status_txt")]
        public string StatusText
        {
            get { return text; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="text"></param>
        public BitNetlyException(string status, string text)
        {
            this.status = status;
            this.text = text;
        }
    }
}
