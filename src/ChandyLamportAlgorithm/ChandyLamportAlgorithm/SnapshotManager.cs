using System;
using System.Collections.Generic;

namespace ChandyLamportAlgorithm
{
    public class SnapshotManager
    {
        public List<Process> Processes { get; set; }

        public SnapshotManager(List<Process> processes)
        {
            Processes = processes;
        }

        public GlobalSnapshot CollectSnapshot()
        {
            GlobalSnapshot snapshot = new GlobalSnapshot();
            foreach (Process process in Processes)
            {
                if (process.HasRecordedState)
                {
                    snapshot.AddProcessState(process.ProcessId, process.RecordedLocalState);
                    foreach (Channel channel in process.IncomingChannels)
                    {
                        List<string> messagesContent = new List<string>();
                        if (process.RecordedChannelMessages.ContainsKey(channel))
                        {
                            foreach (Message msg in process.RecordedChannelMessages[channel])
                            {
                                messagesContent.Add(msg.Content);
                            }
                        }

                        snapshot.AddChannelState(channel.GetIdentifier(), messagesContent);
                    }
                }
            }

            return snapshot;
        }
    }
}