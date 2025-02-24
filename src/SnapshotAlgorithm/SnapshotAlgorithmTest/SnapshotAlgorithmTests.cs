using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using SnapshotAlgorithm;

namespace SnapshotAlgorithmTest
{
    [TestClass]
    public class SnapshotAlgorithmTests
    {
        [TestMethod]
        public void TestTakeSnapshotEmpty()
        {
            List<ISnapshotable> processes = new List<ISnapshotable>();
            SnapshotManager manager = new SnapshotManager(processes);
            Snapshot snapshot = manager.TakeSnapshot();
            Assert.IsNotNull(snapshot);
            Assert.AreEqual(0, snapshot.ProcessStates.Count);
        }

        [TestMethod]
        public void TestTakeSnapshotWithProcesses()
        {
            List<ISnapshotable> processes = new List<ISnapshotable>
            {
                new Process("Test1", "State1"),
                new Process("Test2", "State2")
            };
            SnapshotManager manager = new SnapshotManager(processes);
            Snapshot snapshot = manager.TakeSnapshot();
            Assert.AreEqual(2, snapshot.ProcessStates.Count);
            Assert.AreEqual("State1", snapshot.ProcessStates["Test1"]);
            Assert.AreEqual("State2", snapshot.ProcessStates["Test2"]);
        }

        [TestMethod]
        public void TestTakeSnapshotLargeDataset()
        {
            List<ISnapshotable> processes = new List<ISnapshotable>();
            for (int i = 0; i < 1000; i++)
            {
                string id = "Proc" + i.ToString();
                string state = "State" + i.ToString();
                processes.Add(new Process(id, state));
            }

            SnapshotManager manager = new SnapshotManager(processes);
            Snapshot snapshot = manager.TakeSnapshot();
            Assert.AreEqual(1000, snapshot.ProcessStates.Count);
            Assert.IsTrue(snapshot.ProcessStates.ContainsKey("Proc0"));
            Assert.AreEqual("State0", snapshot.ProcessStates["Proc0"]);
            Assert.IsTrue(snapshot.ProcessStates.ContainsKey("Proc500"));
            Assert.AreEqual("State500", snapshot.ProcessStates["Proc500"]);
            Assert.IsTrue(snapshot.ProcessStates.ContainsKey("Proc999"));
            Assert.AreEqual("State999", snapshot.ProcessStates["Proc999"]);
        }
    }
}