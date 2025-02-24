using System;

namespace DijkstraScholtenAlgorithm
{
    public class ProcessNode
    {
        public string Id { get; }
        public ProcessNode? Parent { get; private set; }
        public int Credit { get; private set; }
        public bool Active { get; private set; }

        public ProcessNode(string id)
        {
            this.Id = id;
            this.Active = false;
            this.Credit = 0;
            this.Parent = null;
        }

        public void Activate(ProcessNode? via)
        {
            if (!this.Active)
            {
                this.Active = true;
                if (via != null)
                {
                    this.Parent = via;
                }
            }
        }

        public void SendWork(ProcessNode target)
        {
            this.Credit++;
            target.ReceiveWork(this);
        }

        public void ReceiveWork(ProcessNode sender)
        {
            if (!this.Active)
            {
                this.Activate(sender);
            }
        }

        public void WorkDone()
        {
            if (this.Credit == 0)
            {
                if (this.Parent != null)
                {
                    this.Parent.ReceiveAck();
                    this.Parent = null;
                }

                this.Active = false;
            }
        }

        public void ReceiveAck()
        {
            if (this.Credit > 0)
            {
                this.Credit--;
            }

            if (this.Credit == 0 && !this.Active && this.Parent != null)
            {
                this.Parent.ReceiveAck();
                this.Parent = null;
            }
        }
    }
}