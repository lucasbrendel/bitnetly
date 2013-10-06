﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitNetly
{
    public interface IBitResponse
    {
        StatusCode StatusCode { get; }
        string LongUrl { get; }
        string ShortUrl { get; }
    }
}
