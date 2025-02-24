using System;
using System.Collections.Generic;

namespace ChandyLamportAlgorithm
{
    public class GlobalSnapshot
    {
        public Dictionary<int, string> ProcessStates { get; set; }
        public Dictionary<string, List<string>> ChannelStates { get; set; }

        public GlobalSnapshot()
        {
            ProcessStates = new Dictionary<int, string>();
            ChannelStates = new Dictionary<string, List<string>>();
        }

        public void AddProcessState(int processId, string state)
        {
            ProcessStates[processId] = state;
        }

        public void AddChannelState(string channelId, List<string> messages)
        {
            ChannelStates[channelId] = messages;
        }

        public void PrintSnapshot()
        {
            Console.WriteLine("Global Snapshot:");
            foreach (KeyValuePair<int, string> kvp in ProcessStates)
            {
                Console.WriteLine("Process " + kvp.Key + ": " + kvp.Value);
            }

            foreach (KeyValuePair<string, List<string>> kvp in ChannelStates)
            {
                Console.Write("Channel " + kvp.Key + ": ");
                foreach (string msg in kvp.Value)
                {
                    Console.Write(msg + " ");
                }

                Console.WriteLine();
            }
        }
    }
}