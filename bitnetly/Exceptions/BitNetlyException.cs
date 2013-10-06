using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;

namespace BitNetly.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class BitNetlyException : Exception
    {
        private string status;

        private string text;

        /// <summary>
        /// 
        /// </summary>
        public string Status
        {
            get { return status; }
        }

        /// <summary>
        /// 
        /// </summary>
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
