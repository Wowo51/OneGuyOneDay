using System.Collections.Generic;

namespace SnapshotAlgorithm
{
    public class Snapshot
    {
        public Dictionary<string, string> ProcessStates { get; set; }

        public Snapshot()
        {
            this.ProcessStates = new Dictionary<string, string>();
        }
    }
}