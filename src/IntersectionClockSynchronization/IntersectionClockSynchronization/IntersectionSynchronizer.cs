using System.Collections.Generic;

namespace IntersectionClockSynchronization
{
    public class IntersectionSynchronizer
    {
        public TimeInterval? ComputeIntersection(List<TimeInterval> intervals)
        {
            if (intervals == null || intervals.Count == 0)
            {
                return null;
            }

            double intersectionStart = intervals[0].Start;
            double intersectionEnd = intervals[0].End;
            for (int i = 1; i < intervals.Count; i++)
            {
                if (intervals[i] == null)
                {
                    continue;
                }

                if (intervals[i].Start > intersectionStart)
                {
                    intersectionStart = intervals[i].Start;
                }

                if (intervals[i].End < intersectionEnd)
                {
                    intersectionEnd = intervals[i].End;
                }

                if (intersectionStart > intersectionEnd)
                {
                    return null;
                }
            }

            return new TimeInterval(intersectionStart, intersectionEnd);
        }
    }
}