using System;
using System.Collections.Generic;

namespace SnapshotAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            System.Collections.Generic.List<ISnapshotable> processes = new System.Collections.Generic.List<ISnapshotable>();
            processes.Add(new Process("Process1", "Active"));
            processes.Add(new Process("Process2", "Idle"));
            SnapshotManager manager = new SnapshotManager(processes);
            Snapshot snapshot = manager.TakeSnapshot();
            foreach (System.Collections.Generic.KeyValuePair<string, string> entry in snapshot.ProcessStates)
            {
                Console.WriteLine(entry.Key + ": " + entry.Value);
            }
        }
    }
}