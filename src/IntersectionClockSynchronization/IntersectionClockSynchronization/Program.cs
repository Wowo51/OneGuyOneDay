using System;
using System.Collections.Generic;

namespace IntersectionClockSynchronization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<TimeInterval> intervals = new List<TimeInterval>();
            intervals.Add(new TimeInterval(1.0, 10.0));
            intervals.Add(new TimeInterval(3.0, 8.0));
            IntersectionSynchronizer synchronizer = new IntersectionSynchronizer();
            TimeInterval? intersection = synchronizer.ComputeIntersection(intervals);
            if (intersection != null)
            {
                Console.WriteLine("Intersection: " + intersection.ToString());
            }
            else
            {
                Console.WriteLine("No intersection found");
            }
        }
    }
}