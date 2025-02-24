using System;

namespace EarliestDeadlineFirst
{
    public class ScheduledTask
    {
        public string Name { get; set; }
        public DateTime Deadline { get; set; }
        public Action TaskAction { get; set; }

        public ScheduledTask(string name, DateTime deadline, Action taskAction)
        {
            this.Name = name ?? "";
            this.Deadline = deadline;
            this.TaskAction = taskAction;
        }
    }
}