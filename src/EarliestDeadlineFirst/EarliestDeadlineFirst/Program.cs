using System;

namespace EarliestDeadlineFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            EdfScheduler scheduler = new EdfScheduler();
            ScheduledTask task1 = new ScheduledTask("Task1", DateTime.Now.AddSeconds(5), new Action(() =>
            {
                Console.WriteLine("Executing Task1");
            }));
            ScheduledTask task2 = new ScheduledTask("Task2", DateTime.Now.AddSeconds(3), new Action(() =>
            {
                Console.WriteLine("Executing Task2");
            }));
            ScheduledTask task3 = new ScheduledTask("Task3", DateTime.Now.AddSeconds(10), new Action(() =>
            {
                Console.WriteLine("Executing Task3");
            }));
            scheduler.AddTask(task1);
            scheduler.AddTask(task2);
            scheduler.AddTask(task3);
            scheduler.ExecuteAllTasks();
        }
    }
}