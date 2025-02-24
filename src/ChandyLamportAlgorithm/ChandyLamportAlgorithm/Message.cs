using System;

namespace ChandyLamportAlgorithm
{
    public enum MessageType
    {
        Application,
        Marker
    }

    public class Message
    {
        public MessageType MessageType { get; set; }
        public string Content { get; set; }
        public string SnapshotId { get; set; }

        public Message(MessageType messageType, string content)
        {
            MessageType = messageType;
            Content = content;
            SnapshotId = "";
        }

        public Message(string snapshotId) : this(MessageType.Marker, "Marker")
        {
            SnapshotId = snapshotId;
        }
    }
}