using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HonestBobs.Web.Infrastructure
{
    public class HttpCacheConfiguration
    {
        public TimeSpan TimeToLive
        {
            get { return new TimeSpan(0, 10, 0); }
        }
    }
}