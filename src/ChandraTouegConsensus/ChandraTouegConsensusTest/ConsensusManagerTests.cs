using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChandraTouegConsensus;

namespace ChandraTouegConsensusTest
{
    [TestClass]
    public class ConsensusManagerTests
    {
        [TestMethod]
        public void TestConsensusWithAllProcessesAlive()
        {
            List<Process> processList = new List<Process>
            {
                new Process(1, 10, true),
                new Process(2, 20, true),
                new Process(3, 30, true)
            };
            ConsensusManager consensusManager = new ConsensusManager(processList);
            int? consensus = consensusManager.RunConsensus();
            Assert.IsNotNull(consensus, "Consensus should be reached when processes are alive.");
            Assert.AreEqual(10, consensus.Value, "Consensus value should be the first alive process's value.");
            foreach (Process proc in processList)
            {
                if (proc.IsAlive)
                {
                    Assert.AreEqual(10, proc.CurrentEstimate, "All alive processes should have the consensus value.");
                    Assert.AreEqual(10, proc.DecidedValue, "All alive processes should record the decision.");
                }
            }
        }

        [TestMethod]
        public void TestConsensusWhenNoProcessesAlive()
        {
            List<Process> processList = new List<Process>
            {
                new Process(1, 10, false),
                new Process(2, 20, false)
            };
            ConsensusManager consensusManager = new ConsensusManager(processList);
            int? consensus = consensusManager.RunConsensus();
            Assert.IsNull(consensus, "Consensus should be null if no processes are alive.");
            foreach (Process proc in processList)
            {
                Assert.IsNull(proc.DecidedValue, "No process should have a decided value when consensus is not reached.");
            }
        }

        [TestMethod]
        public void TestConsensusWithMixedAliveProcesses()
        {
            List<Process> processList = new List<Process>
            {
                new Process(5, 50, false),
                new Process(2, 20, true),
                new Process(4, 40, false),
                new Process(1, 10, true)
            };
            ConsensusManager consensusManager = new ConsensusManager(processList);
            int? consensus = consensusManager.RunConsensus();
            Assert.IsNotNull(consensus, "Consensus should be reached with some alive processes.");
            Assert.AreEqual(20, consensus.Value, "Consensus value should be the first alive process's value in list order.");
            foreach (Process proc in processList)
            {
                if (proc.IsAlive)
                {
                    Assert.AreEqual(20, proc.CurrentEstimate, "All alive processes should have the consensus value.");
                    Assert.AreEqual(20, proc.DecidedValue, "All alive processes should record the decision.");
                }
            }
        }

        [TestMethod]
        public void TestConsensusWithLargeNumberOfProcesses()
        {
            List<Process> processList = new List<Process>();
            for (int i = 0; i < 1000; i++)
            {
                bool isAlive = (i % 2 == 0);
                Process newProcess = new Process(i + 1, i, isAlive);
                processList.Add(newProcess);
            }

            ConsensusManager consensusManager = new ConsensusManager(processList);
            int? consensus = consensusManager.RunConsensus();
            if (consensus != null)
            {
                int expected = processList[0].CurrentEstimate;
                Assert.AreEqual(expected, consensus.Value, "Consensus value should match the first alive process's value.");
                foreach (Process proc in processList)
                {
                    if (proc.IsAlive)
                    {
                        Assert.AreEqual(expected, proc.CurrentEstimate, "All alive processes should be updated to consensus value.");
                        Assert.AreEqual(expected, proc.DecidedValue, "All alive processes should record the consensus decision.");
                    }
                }
            }
            else
            {
                bool anyAlive = false;
                foreach (Process proc in processList)
                {
                    if (proc.IsAlive)
                    {
                        anyAlive = true;
                        break;
                    }
                }

                Assert.IsFalse(anyAlive, "Consensus is null only when no processes are alive.");
            }
        }
    }
}