using System;
using System.Collections.Generic;

namespace ShortestSeekFirst
{
    public class SSTFScheduler
    {
        public static List<int> Schedule(List<int> requests, int head)
        {
            if (requests == null)
            {
                return new List<int>();
            }

            List<int> remaining = new List<int>(requests);
            List<int> sequence = new List<int>();
            int currentPosition = head;
            while (remaining.Count > 0)
            {
                int closestRequest = remaining[0];
                int closestDistance = Math.Abs(currentPosition - closestRequest);
                foreach (int request in remaining)
                {
                    int distance = Math.Abs(currentPosition - request);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestRequest = request;
                    }
                }

                sequence.Add(closestRequest);
                currentPosition = closestRequest;
                remaining.Remove(closestRequest);
            }

            return sequence;
        }
    }
}