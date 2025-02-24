using System;

namespace AriesRecovery
{
    public enum LogType
    {
        Begin,
        Update,
        Commit,
        Abort,
        CLR
    }

    public class LogRecord
    {
        public long LSN { get; set; }
        public long? PrevLSN { get; set; }
        public int? PageId { get; set; }
        public LogType RecordType { get; set; }
        public int TransactionId { get; set; }
        public string Operation { get; set; }
        public DateTime Timestamp { get; set; }

        public LogRecord(int transactionId, string operation, DateTime timestamp)
        {
            TransactionId = transactionId;
            Operation = operation;
            Timestamp = timestamp;
            // LSN will be assigned by LogManager if not preset.
            LSN = 0;
            PrevLSN = null;
            PageId = null;
            RecordType = LogType.Begin;
        }

        public LogRecord(int transactionId, string operation, DateTime timestamp, long lsn, long? prevLSN, int? pageId, LogType recordType)
        {
            TransactionId = transactionId;
            Operation = operation;
            Timestamp = timestamp;
            LSN = lsn;
            PrevLSN = prevLSN;
            PageId = pageId;
            RecordType = recordType;
        }
    }
}