using System;

namespace CristianAlgorithmSync
{
    public class TimeServer : ITimeServer
    {
        public DateTime GetServerTime()
        {
            return DateTime.UtcNow;
        }
    }
}