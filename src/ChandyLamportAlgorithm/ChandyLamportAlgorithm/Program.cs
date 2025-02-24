using System;
using System.Collections.Generic;

namespace ChandyLamportAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create processes.
            ChandyLamportAlgorithm.Process process1 = new ChandyLamportAlgorithm.Process(1);
            ChandyLamportAlgorithm.Process process2 = new ChandyLamportAlgorithm.Process(2);
            ChandyLamportAlgorithm.Process process3 = new ChandyLamportAlgorithm.Process(3);
            // Create channels between processes.
            Channel channel1to2 = new Channel(process1, process2);
            Channel channel2to1 = new Channel(process2, process1);
            Channel channel1to3 = new Channel(process1, process3);
            Channel channel3to1 = new Channel(process3, process1);
            Channel channel2to3 = new Channel(process2, process3);
            Channel channel3to2 = new Channel(process3, process2);
            // Add channels to processes.
            process1.AddOutgoingChannel(channel1to2);
            process1.AddOutgoingChannel(channel1to3);
            process1.AddIncomingChannel(channel2to1);
            process1.AddIncomingChannel(channel3to1);
            process2.AddOutgoingChannel(channel2to1);
            process2.AddOutgoingChannel(channel2to3);
            process2.AddIncomingChannel(channel1to2);
            process2.AddIncomingChannel(channel3to2);
            process3.AddOutgoingChannel(channel3to1);
            process3.AddOutgoingChannel(channel3to2);
            process3.AddIncomingChannel(channel1to3);
            process3.AddIncomingChannel(channel2to3);
            // Simulate application messages before snapshot.
            Message appMessage1 = new Message(MessageType.Application, "AppMsg P1 to P2");
            channel1to2.Send(appMessage1);
            Message appMessage2 = new Message(MessageType.Application, "AppMsg P3 to P1");
            channel3to1.Send(appMessage2);
            // Initiate snapshot at process1.
            process1.StartSnapshot("snapshot1");
            // Send more application messages after snapshot has started.
            Message appMessage3 = new Message(MessageType.Application, "AppMsg P2 to P1 after snapshot");
            channel2to1.Send(appMessage3);
            Message appMessage4 = new Message(MessageType.Application, "AppMsg P1 to P3 after snapshot");
            channel1to3.Send(appMessage4);
            // Collect global snapshot.
            List<ChandyLamportAlgorithm.Process> processes = new List<ChandyLamportAlgorithm.Process>
            {
                process1,
                process2,
                process3
            };
            SnapshotManager snapshotManager = new SnapshotManager(processes);
            GlobalSnapshot globalSnapshot = snapshotManager.CollectSnapshot();
            globalSnapshot.PrintSnapshot();
        }
    }
}