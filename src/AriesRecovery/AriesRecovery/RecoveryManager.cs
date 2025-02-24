using System;
using System.Collections.Generic;

namespace AriesRecovery
{
    public class RecoveryManager
    {
        private LogManager _logManager;
        private Dictionary<int, long> _transactionTable;
        private Dictionary<int, long> _dirtyPageTable;
        public RecoveryManager(LogManager logManager)
        {
            _logManager = logManager;
            _transactionTable = new Dictionary<int, long>();
            _dirtyPageTable = new Dictionary<int, long>();
        }

        public void Recover()
        {
            Analyze();
            Redo();
            Undo();
        }

        private void Analyze()
        {
            Console.WriteLine("ARIES Analysis phase initiated...");
            List<LogRecord> logs = _logManager.ReadLog();
            Dictionary<int, long> transactionTable = new Dictionary<int, long>();
            Dictionary<int, long> dirtyPageTable = new Dictionary<int, long>();
            foreach (LogRecord record in logs)
            {
                if (record.RecordType == LogType.Begin)
                {
                    transactionTable[record.TransactionId] = record.LSN;
                }
                else if (record.RecordType == LogType.Update)
                {
                    // Update or add the last LSN for the transaction.
                    transactionTable[record.TransactionId] = record.LSN;
                    // For update records, if a page is involved, record its recLSN.
                    if (record.PageId.HasValue)
                    {
                        int pageId = record.PageId.Value;
                        if (!dirtyPageTable.ContainsKey(pageId))
                        {
                            dirtyPageTable[pageId] = record.LSN;
                        }
                    }
                }
                else if (record.RecordType == LogType.Commit || record.RecordType == LogType.Abort)
                {
                    if (transactionTable.ContainsKey(record.TransactionId))
                    {
                        transactionTable.Remove(record.TransactionId);
                    }
                }
            // CLR records are part of undo logging; no transaction table update needed.
            }

            Console.WriteLine("Analysis complete. Active transactions: " + transactionTable.Count + ", Dirty pages: " + dirtyPageTable.Count);
            _transactionTable = transactionTable;
            _dirtyPageTable = dirtyPageTable;
        }

        private void Redo()
        {
            Console.WriteLine("ARIES Redo phase initiated...");
            List<LogRecord> logs = _logManager.ReadLog();
            long startLSN = long.MaxValue;
            foreach (KeyValuePair<int, long> kvp in _dirtyPageTable)
            {
                if (kvp.Value < startLSN)
                {
                    startLSN = kvp.Value;
                }
            }

            if (startLSN == long.MaxValue)
            {
                startLSN = 0;
            }

            foreach (LogRecord record in logs)
            {
                if (record.RecordType == LogType.Update && record.LSN >= startLSN)
                {
                    Console.WriteLine("Redo update operation: " + record.Operation + " on page: " + (record.PageId.HasValue ? record.PageId.ToString() : "N/A"));
                }
            }

            Console.WriteLine("Redo phase complete.");
        }

        private void Undo()
        {
            Console.WriteLine("ARIES Undo phase initiated...");
            List<LogRecord> logs = _logManager.ReadLog();
            for (int i = logs.Count - 1; i >= 0; i--)
            {
                LogRecord record = logs[i];
                if (record.RecordType == LogType.Update && _transactionTable.ContainsKey(record.TransactionId))
                {
                    Console.WriteLine("Undo update operation: " + record.Operation + " on page: " + (record.PageId.HasValue ? record.PageId.ToString() : "N/A"));
                    LogRecord clr = new LogRecord(record.TransactionId, "CLR for " + record.Operation, DateTime.Now, 0, record.LSN, record.PageId, LogType.CLR);
                    _logManager.WriteLog(clr);
                }
            }

            Console.WriteLine("Undo phase complete.");
        }
    }
}