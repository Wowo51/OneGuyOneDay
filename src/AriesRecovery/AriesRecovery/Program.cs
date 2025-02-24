using System;

namespace AriesRecovery
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LogManager logManager = new LogManager();
            // Transaction 1: An uncommitted transaction to demonstrate undo.
            LogRecord t1Begin = new LogRecord(1, "Begin Transaction", DateTime.Now);
            t1Begin.RecordType = LogType.Begin;
            logManager.WriteLog(t1Begin);
            LogRecord t1Update = new LogRecord(1, "Update record on page 10", DateTime.Now);
            t1Update.PageId = 10;
            t1Update.RecordType = LogType.Update;
            logManager.WriteLog(t1Update);
            // Transaction 2: A committed transaction that will not be undone.
            LogRecord t2Begin = new LogRecord(2, "Begin Transaction", DateTime.Now);
            t2Begin.RecordType = LogType.Begin;
            logManager.WriteLog(t2Begin);
            LogRecord t2Update = new LogRecord(2, "Update record on page 20", DateTime.Now);
            t2Update.PageId = 20;
            t2Update.RecordType = LogType.Update;
            logManager.WriteLog(t2Update);
            LogRecord t2Commit = new LogRecord(2, "Commit Transaction", DateTime.Now);
            t2Commit.RecordType = LogType.Commit;
            logManager.WriteLog(t2Commit);
            RecoveryManager recoveryManager = new RecoveryManager(logManager);
            recoveryManager.Recover();
            Console.WriteLine("Recovery process completed.");
        }
    }
}