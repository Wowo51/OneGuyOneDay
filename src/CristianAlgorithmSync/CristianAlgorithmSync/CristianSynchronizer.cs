using System;

namespace CristianAlgorithmSync
{
    public class CristianSynchronizer
    {
        public DateTime SynchronizeTime(ITimeServer timeServer)
        {
            if (timeServer == null)
            {
                return DateTime.UtcNow;
            }

            DateTime t0 = DateTime.UtcNow;
            DateTime serverTime = timeServer.GetServerTime();
            DateTime t1 = DateTime.UtcNow;
            TimeSpan delay = t1 - t0;
            return serverTime.AddTicks(delay.Ticks / 2);
        }
    }
}