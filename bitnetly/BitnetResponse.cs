using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bitnetly
{
    class BitnetResponse : IBitResponse
    {
        #region IBitResponse Members

        public StatusCode StatusCode
        {
            get;
            set;
        }

        public string LongUrl
        {
            get;
            set;
        }

        public string ShortUrl
        {
            get;
            set;
        }

        #endregion
    }
}
