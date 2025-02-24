using System.Collections.Generic;
using System.Linq;

namespace ShortestJobScheduling
{
    public class ShortestJobNextScheduler : IScheduler
    {
        public List<Job> Schedule(List<Job> jobs)
        {
            if (jobs == null)
            {
                return new List<Job>();
            }

            List<Job> scheduledJobs = jobs.OrderBy(job => job.Duration).ToList();
            return scheduledJobs;
        }
    }
}