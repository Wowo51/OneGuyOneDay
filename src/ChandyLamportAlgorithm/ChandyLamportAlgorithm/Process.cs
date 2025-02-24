using System;
using System.Collections.Generic;

namespace ChandyLamportAlgorithm
{
    public class Process
    {
        public int ProcessId { get; set; }
        public string LocalState { get; set; }
        public string RecordedLocalState { get; set; }
        public List<Channel> IncomingChannels { get; set; }
        public List<Channel> OutgoingChannels { get; set; }
        public bool HasRecordedState { get; set; }
        public Dictionary<Channel, List<Message>> RecordedChannelMessages { get; set; }
        public Dictionary<Channel, bool> IsRecordingChannel { get; set; }

        public Process(int processId)
        {
            ProcessId = processId;
            LocalState = "Initial state of process " + processId.ToString();
            RecordedLocalState = "";
            IncomingChannels = new List<Channel>();
            OutgoingChannels = new List<Channel>();
            HasRecordedState = false;
            RecordedChannelMessages = new Dictionary<Channel, List<Message>>();
            IsRecordingChannel = new Dictionary<Channel, bool>();
        }

        public void AddIncomingChannel(Channel channel)
        {
            IncomingChannels.Add(channel);
            RecordedChannelMessages[channel] = new List<Message>();
            IsRecordingChannel[channel] = false;
        }

        public void AddOutgoingChannel(Channel channel)
        {
            OutgoingChannels.Add(channel);
        }

        public void StartSnapshot(string snapshotId)
        {
            if (!HasRecordedState)
            {
                RecordLocalState();
                HasRecordedState = true;
                foreach (Channel channel in IncomingChannels)
                {
                    IsRecordingChannel[channel] = true;
                }

                Message marker = new Message(snapshotId);
                foreach (Channel channel in OutgoingChannels)
                {
                    channel.Send(marker);
                }
            }
        }

        public void Receive(Message message, Channel channel)
        {
            if (message.MessageType == MessageType.Marker)
            {
                if (!HasRecordedState)
                {
                    RecordLocalState();
                    HasRecordedState = true;
                    foreach (Channel ch in IncomingChannels)
                    {
                        if (ch == channel)
                        {
                            IsRecordingChannel[ch] = false;
                        }
                        else
                        {
                            IsRecordingChannel[ch] = true;
                        }
                    }

                    Message marker = new Message(message.SnapshotId);
                    foreach (Channel outChannel in OutgoingChannels)
                    {
                        outChannel.Send(marker);
                    }
                }
                else
                {
                    if (IsRecordingChannel.ContainsKey(channel))
                    {
                        IsRecordingChannel[channel] = false;
                    }
                }
            }
            else if (message.MessageType == MessageType.Application)
            {
                if (HasRecordedState && IsRecordingChannel.ContainsKey(channel) && IsRecordingChannel[channel])
                {
                    RecordedChannelMessages[channel].Add(message);
                }

                LocalState = "Updated by message: " + message.Content;
            }
        }

        private void RecordLocalState()
        {
            RecordedLocalState = "Recorded state of process " + ProcessId.ToString();
        }
    }
}