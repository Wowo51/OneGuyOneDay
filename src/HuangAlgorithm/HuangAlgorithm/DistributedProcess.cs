using System;

namespace HuangAlgorithm
{
    public class DistributedProcess
    {
        public string ProcessId;
        public bool IsActive;
        public int SentMessages;
        public int ReceivedMessages;
        public DistributedProcess(string processId)
        {
            ProcessId = processId;
            IsActive = false;
            SentMessages = 0;
            ReceivedMessages = 0;
        }

        public void SendMessage(DistributedProcess receiver)
        {
            SentMessages = SentMessages + 1;
            receiver.ReceiveMessage();
        }

        public void ReceiveMessage()
        {
            ReceivedMessages = ReceivedMessages + 1;
            IsActive = true;
        }

        public void CompleteWork()
        {
            IsActive = false;
        }
    }
}