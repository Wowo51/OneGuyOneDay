using System;

namespace ChandyLamportAlgorithm
{
    public class Channel
    {
        public Process Sender { get; set; }
        public Process Receiver { get; set; }

        public Channel(Process sender, Process receiver)
        {
            Sender = sender;
            Receiver = receiver;
        }

        public void Send(Message message)
        {
            Receiver.Receive(message, this);
        }

        public string GetIdentifier()
        {
            return "Channel_" + Sender.ProcessId.ToString() + "->" + Receiver.ProcessId.ToString();
        }
    }
}