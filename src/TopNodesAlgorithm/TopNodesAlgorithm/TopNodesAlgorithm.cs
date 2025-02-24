using System;
using System.Collections.Generic;

namespace TopNodesAlgorithm
{
    public class TopNodesAlgorithm
    {
        public static List<ResourceCalendarEvent> GetTopNodes(List<ResourceCalendarEvent> events)
        {
            if (events == null)
            {
                return new List<ResourceCalendarEvent>();
            }

            List<ResourceCalendarEvent> topNodes = new List<ResourceCalendarEvent>();
            foreach (ResourceCalendarEvent currentEvent in events)
            {
                bool isContained = false;
                foreach (ResourceCalendarEvent otherEvent in events)
                {
                    if (otherEvent != currentEvent && otherEvent.Start <= currentEvent.Start && otherEvent.End >= currentEvent.End)
                    {
                        isContained = true;
                        break;
                    }
                }

                if (!isContained)
                {
                    topNodes.Add(currentEvent);
                }
            }

            return topNodes;
        }
    }
}