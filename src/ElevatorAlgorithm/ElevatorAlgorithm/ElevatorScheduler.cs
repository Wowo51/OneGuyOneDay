using System;
using System.Collections.Generic;
using System.Linq;

namespace ElevatorAlgorithm
{
    public class ElevatorScheduler
    {
        public List<int> Schedule(int initialPosition, List<int> requests, bool movingUp, int maxCylinder)
        {
            if (requests == null)
            {
                requests = new List<int>();
            }

            List<int> sortedRequests = new List<int>(requests);
            sortedRequests.Sort();
            List<int> result = new List<int>();
            if (movingUp)
            {
                List<int> upRequests = sortedRequests.Where((int x) => x >= initialPosition).ToList();
                List<int> downRequests = sortedRequests.Where((int x) => x < initialPosition).ToList();
                result.AddRange(upRequests);
                downRequests.Reverse();
                result.AddRange(downRequests);
            }
            else
            {
                List<int> downRequests = sortedRequests.Where((int x) => x <= initialPosition).ToList();
                downRequests.Reverse();
                List<int> upRequests = sortedRequests.Where((int x) => x > initialPosition).ToList();
                result.AddRange(downRequests);
                result.AddRange(upRequests);
            }

            return result;
        }
    }
}