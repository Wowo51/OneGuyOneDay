using System;

namespace TopNodesAlgorithm
{
    public class ResourceCalendarEvent
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; }

        public ResourceCalendarEvent(DateTime start, DateTime end, string description)
        {
            this.Start = start;
            this.End = end;
            this.Description = description;
        }
    }
}