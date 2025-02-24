using System;

namespace AriesRecovery
{
    public class Transaction
    {
        public int TransactionId { get; private set; }
        public string Status { get; private set; }

        public Transaction(int transactionId)
        {
            TransactionId = transactionId;
            Status = "Active";
        }

        public void Commit()
        {
            Status = "Committed";
            Console.WriteLine("Transaction " + TransactionId.ToString() + " committed.");
        }

        public void Abort()
        {
            Status = "Aborted";
            Console.WriteLine("Transaction " + TransactionId.ToString() + " aborted.");
        }
    }
}