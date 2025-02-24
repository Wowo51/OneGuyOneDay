using System;
using System.Collections.Generic;
using System.Linq;

namespace EarliestDeadlineFirst
{
    public class EdfScheduler
    {
        private List<ScheduledTask> _tasks = new List<ScheduledTask>();
        public void AddTask(ScheduledTask task)
        {
            if (task != null)
            {
                _tasks.Add(task);
            }
        }

        public void RemoveTask(ScheduledTask task)
        {
            if (task != null)
            {
                _tasks.Remove(task);
            }
        }

        public List<ScheduledTask> GetScheduledTasks()
        {
            return _tasks.OrderBy(task => task.Deadline).ToList();
        }

        public void ExecuteAllTasks()
        {
            List<ScheduledTask> sortedTasks = GetScheduledTasks();
            foreach (ScheduledTask task in sortedTasks)
            {
                if (task.TaskAction != null)
                {
                    task.TaskAction();
                }
            }
        }
    }
}