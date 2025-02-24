using System.Collections.Generic;

namespace SnapshotAlgorithm
{
    public class SnapshotManager
    {
        private System.Collections.Generic.List<ISnapshotable> _processes;
        public SnapshotManager(System.Collections.Generic.List<ISnapshotable> processes)
        {
            this._processes = processes;
        }

        public Snapshot TakeSnapshot()
        {
            Snapshot snapshot = new Snapshot();
            foreach (ISnapshotable process in this._processes)
            {
                snapshot.ProcessStates.Add(process.Id, process.GetState());
            }

            return snapshot;
        }
    }
}