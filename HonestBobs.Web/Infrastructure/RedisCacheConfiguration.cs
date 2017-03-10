using System;
using System.Configuration;

namespace HonestBobs.Web.Infrastructure
{
    public class RedisCacheConfiguration
    {
        public string HostName
        {
            get { return ConfigurationManager.AppSettings["Redis.ServerUrl"]; }
        }

        public TimeSpan TimeToLive
        {
            get { return new TimeSpan(0, 10, 0); }
        }
    }
}