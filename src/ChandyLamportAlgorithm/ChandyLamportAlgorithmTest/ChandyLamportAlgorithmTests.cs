using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChandyLamportAlgorithm;

namespace ChandyLamportAlgorithmTest
{
    [TestClass]
    public class ChandyLamportAlgorithmTests
    {
        [TestMethod]
        public void Test_ChannelSendApplicationMessage()
        {
            ChandyLamportAlgorithm.Process sender = new ChandyLamportAlgorithm.Process(1);
            ChandyLamportAlgorithm.Process receiver = new ChandyLamportAlgorithm.Process(2);
            Channel channel = new Channel(sender, receiver);
            sender.AddOutgoingChannel(channel);
            receiver.AddIncomingChannel(channel);
            Message message = new Message(MessageType.Application, "Hello");
            channel.Send(message);
            Assert.AreEqual("Updated by message: Hello", receiver.LocalState);
        }

        [TestMethod]
        public void Test_ProcessSnapshotRecording()
        {
            ChandyLamportAlgorithm.Process process = new ChandyLamportAlgorithm.Process(1);
            ChandyLamportAlgorithm.Process otherProcess = new ChandyLamportAlgorithm.Process(2);
            Channel channel = new Channel(otherProcess, process);
            process.AddIncomingChannel(channel);
            Assert.IsFalse(process.HasRecordedState);
            process.StartSnapshot("snap1");
            Assert.IsTrue(process.HasRecordedState);
            Assert.AreEqual("Recorded state of process 1", process.RecordedLocalState);
            Message marker = new Message("snap1");
            process.Receive(marker, channel);
            bool isRecording = false;
            if (process.IsRecordingChannel.ContainsKey(channel))
            {
                isRecording = process.IsRecordingChannel[channel];
            }

            Assert.IsFalse(isRecording);
        }

        [TestMethod]
        public void Test_GlobalSnapshotCollection()
        {
            ChandyLamportAlgorithm.Process p1 = new ChandyLamportAlgorithm.Process(1);
            ChandyLamportAlgorithm.Process p2 = new ChandyLamportAlgorithm.Process(2);
            ChandyLamportAlgorithm.Process p3 = new ChandyLamportAlgorithm.Process(3);
            Channel c1to2 = new Channel(p1, p2);
            Channel c1to3 = new Channel(p1, p3);
            Channel c2to1 = new Channel(p2, p1);
            p1.AddOutgoingChannel(c1to2);
            p1.AddOutgoingChannel(c1to3);
            p2.AddIncomingChannel(c1to2);
            p3.AddIncomingChannel(c1to3);
            p2.AddOutgoingChannel(c2to1);
            p1.AddIncomingChannel(c2to1);
            p1.StartSnapshot("snap2");
            p2.StartSnapshot("snap2");
            p3.StartSnapshot("snap2");
            Message marker1 = new Message("snap2");
            p1.Receive(marker1, c2to1);
            p2.Receive(marker1, c1to2);
            p3.Receive(marker1, c1to3);
            List<ChandyLamportAlgorithm.Process> processes = new List<ChandyLamportAlgorithm.Process>
            {
                p1,
                p2,
                p3
            };
            SnapshotManager snapshotManager = new SnapshotManager(processes);
            GlobalSnapshot globalSnapshot = snapshotManager.CollectSnapshot();
            Assert.IsTrue(globalSnapshot.ProcessStates.ContainsKey(1));
            Assert.IsTrue(globalSnapshot.ProcessStates.ContainsKey(2));
            Assert.IsTrue(globalSnapshot.ProcessStates.ContainsKey(3));
            Assert.AreEqual("Recorded state of process 1", globalSnapshot.ProcessStates[1]);
            Assert.AreEqual("Recorded state of process 2", globalSnapshot.ProcessStates[2]);
            Assert.AreEqual("Recorded state of process 3", globalSnapshot.ProcessStates[3]);
        }

        [TestMethod]
        public void Test_SyntheticLargeNetworkSnapshot()
        {
            const int numProcesses = 50;
            List<ChandyLamportAlgorithm.Process> processes = new List<ChandyLamportAlgorithm.Process>();
            for (int i = 1; i <= numProcesses; i++)
            {
                ChandyLamportAlgorithm.Process proc = new ChandyLamportAlgorithm.Process(i);
                processes.Add(proc);
            }

            for (int i = 0; i < numProcesses; i++)
            {
                int next = (i + 1) % numProcesses;
                Channel channel = new Channel(processes[i], processes[next]);
                processes[i].AddOutgoingChannel(channel);
                processes[next].AddIncomingChannel(channel);
            }

            foreach (ChandyLamportAlgorithm.Process proc in processes)
            {
                foreach (Channel channel in proc.OutgoingChannels)
                {
                    Message appMessage = new Message(MessageType.Application, "LargeNetworkMessage from process " + proc.ProcessId.ToString());
                    channel.Send(appMessage);
                }
            }

            foreach (ChandyLamportAlgorithm.Process proc in processes)
            {
                proc.StartSnapshot("largeSnap");
            }

            foreach (ChandyLamportAlgorithm.Process proc in processes)
            {
                foreach (Channel channel in proc.IncomingChannels)
                {
                    Message marker = new Message("largeSnap");
                    proc.Receive(marker, channel);
                }
            }

            SnapshotManager snapshotManager = new SnapshotManager(processes);
            GlobalSnapshot globalSnapshot = snapshotManager.CollectSnapshot();
            Assert.AreEqual(numProcesses, globalSnapshot.ProcessStates.Count);
        }
    }
}