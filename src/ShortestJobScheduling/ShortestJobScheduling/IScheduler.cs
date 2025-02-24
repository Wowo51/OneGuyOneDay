using System.Collections.Generic;

namespace ShortestJobScheduling
{
    public interface IScheduler
    {
        List<Job> Schedule(List<Job> jobs);
    }
}