using System;
using System.Collections.Generic;
using System.Linq;
using BullyAlgorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BullyAlgorithmTest
{
    [TestClass]
    public class BullyElectionTests
    {
        [TestMethod]
        public void Test_AllProcessesAlive_Election()
        {
            List<Process> processes = new List<Process>
            {
                new Process(1),
                new Process(2),
                new Process(3),
                new Process(4),
                new Process(5)
            };
            BullyElection election = new BullyElection(processes);
            Process initiator = processes[0];
            Process? coordinator = election.ElectCoordinator(initiator);
            Assert.IsNotNull(coordinator);
            Assert.AreEqual(5, coordinator.ProcessId);
            foreach (Process process in processes)
            {
                if (process.ProcessId == 5)
                {
                    Assert.IsTrue(process.IsCoordinator);
                }
                else
                {
                    Assert.IsFalse(process.IsCoordinator);
                }
            }
        }

        [TestMethod]
        public void Test_SomeProcessesDown_Election()
        {
            List<Process> processes = new List<Process>
            {
                new Process(1),
                new Process(2),
                new Process(3),
                new Process(4),
                new Process(5)
            };
            Process? process5 = processes.Find(p => p.ProcessId == 5);
            Assert.IsNotNull(process5);
            process5!.IsAlive = false;
            BullyElection election = new BullyElection(processes);
            Process initiator = processes[1]; // Process with ID 2
            Process? coordinator = election.ElectCoordinator(initiator);
            Assert.IsNotNull(coordinator);
            Assert.AreEqual(4, coordinator.ProcessId);
            foreach (Process process in processes)
            {
                if (process.ProcessId == 4)
                {
                    Assert.IsTrue(process.IsCoordinator);
                }
                else
                {
                    Assert.IsFalse(process.IsCoordinator);
                }
            }
        }

        [TestMethod]
        public void Test_AllProcessesDown_Election()
        {
            List<Process> processes = new List<Process>
            {
                new Process(1),
                new Process(2),
                new Process(3)
            };
            foreach (Process process in processes)
            {
                process.IsAlive = false;
            }

            BullyElection election = new BullyElection(processes);
            Process initiator = processes[0];
            Process? coordinator = election.ElectCoordinator(initiator);
            Assert.IsNull(coordinator);
            foreach (Process process in processes)
            {
                Assert.IsFalse(process.IsCoordinator);
            }
        }

        [TestMethod]
        public void Test_StartElection_Method()
        {
            List<Process> processes = new List<Process>
            {
                new Process(10),
                new Process(20),
                new Process(30)
            };
            BullyElection election = new BullyElection(processes);
            Process initiator = processes[1]; // Process with ID 20
            Process? coordinator = initiator.StartElection(election);
            Assert.IsNotNull(coordinator);
            Assert.AreEqual(30, coordinator.ProcessId);
            foreach (Process process in processes)
            {
                if (process.ProcessId == 30)
                {
                    Assert.IsTrue(process.IsCoordinator);
                }
                else
                {
                    Assert.IsFalse(process.IsCoordinator);
                }
            }
        }

        [TestMethod]
        public void Test_LargeDataset_Election()
        {
            List<Process> processes = new List<Process>();
            for (int i = 1; i <= 1000; i++)
            {
                Process proc = new Process(i);
                proc.IsAlive = (i % 2 == 0);
                processes.Add(proc);
            }

            BullyElection election = new BullyElection(processes);
            Process initiator = processes[0];
            Process? coordinator = election.ElectCoordinator(initiator);
            Assert.IsNotNull(coordinator);
            int expectedCoordinatorId = processes.Where(p => p.IsAlive).Max(p => p.ProcessId);
            Assert.AreEqual(expectedCoordinatorId, coordinator.ProcessId);
            foreach (Process process in processes)
            {
                if (process.ProcessId == expectedCoordinatorId)
                {
                    Assert.IsTrue(process.IsCoordinator);
                }
                else
                {
                    Assert.IsFalse(process.IsCoordinator);
                }
            }
        }
    }
}