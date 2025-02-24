using System;

namespace IntersectionClockSynchronization
{
    public class TimeInterval
    {
        public double Start { get; set; }
        public double End { get; set; }

        public TimeInterval(double start, double end)
        {
            Start = start;
            End = end;
        }

        public override string ToString()
        {
            return $"[{Start}, {End}]";
        }
    }
}