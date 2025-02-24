using System;

namespace ShortestJobScheduling
{
    public class Job
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }

        public Job(int id, string name, int duration)
        {
            this.Id = id;
            this.Name = name;
            this.Duration = duration;
        }
    }
}