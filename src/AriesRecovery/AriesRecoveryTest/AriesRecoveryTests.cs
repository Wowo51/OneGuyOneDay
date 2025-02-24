using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AriesRecovery;

namespace AriesRecoveryTest
{
    [TestClass]
    public class AriesRecoveryTests
    {
        [TestMethod]
        public void Test_LogManager_Assign_LSN()
        {
            LogManager logManager = new LogManager();
            LogRecord record = new LogRecord(1, "Test Operation", DateTime.UtcNow);
            record.RecordType = LogType.Update;
            record.PageId = 100;
            logManager.WriteLog(record);
            List<LogRecord> logs = logManager.ReadLog();
            Assert.IsTrue(logs.Count == 1, "There should be one log record.");
            Assert.IsTrue(logs[0].LSN != 0, "LSN should have been assigned a non-zero value.");
        }

        [TestMethod]
        public void Test_LogManager_ReadLog_Count()
        {
            LogManager logManager = new LogManager();
            for (int i = 0; i < 5; i++)
            {
                LogRecord record = new LogRecord(i + 1, "Operation " + (i + 1), DateTime.UtcNow);
                record.RecordType = LogType.Update;
                record.PageId = 10 * (i + 1);
                logManager.WriteLog(record);
            }

            List<LogRecord> logs = logManager.ReadLog();
            Assert.AreEqual(5, logs.Count, "Log count should be 5.");
        }

        [TestMethod]
        public void Test_RecoveryManager_Undo_Creates_CLR_Logs()
        {
            LogManager logManager = new LogManager();
            // Transaction 1: Uncommitted
            LogRecord t1Begin = new LogRecord(1, "Begin Transaction", DateTime.UtcNow);
            t1Begin.RecordType = LogType.Begin;
            logManager.WriteLog(t1Begin);
            LogRecord t1Update = new LogRecord(1, "Update record on page 50", DateTime.UtcNow);
            t1Update.RecordType = LogType.Update;
            t1Update.PageId = 50;
            logManager.WriteLog(t1Update);
            // Transaction 2: Committed
            LogRecord t2Begin = new LogRecord(2, "Begin Transaction", DateTime.UtcNow);
            t2Begin.RecordType = LogType.Begin;
            logManager.WriteLog(t2Begin);
            LogRecord t2Update = new LogRecord(2, "Update record on page 70", DateTime.UtcNow);
            t2Update.RecordType = LogType.Update;
            t2Update.PageId = 70;
            logManager.WriteLog(t2Update);
            LogRecord t2Commit = new LogRecord(2, "Commit Transaction", DateTime.UtcNow);
            t2Commit.RecordType = LogType.Commit;
            logManager.WriteLog(t2Commit);
            RecoveryManager recoveryManager = new RecoveryManager(logManager);
            recoveryManager.Recover();
            List<LogRecord> logs = logManager.ReadLog();
            int clrCount = 0;
            for (int i = 0; i < logs.Count; i++)
            {
                if (logs[i].RecordType == LogType.CLR)
                {
                    clrCount++;
                }
            }

            Assert.IsTrue(clrCount >= 1, "At least one CLR log should be created for uncommitted transactions.");
        }

        [TestMethod]
        public void Test_Transaction_CommitAndAbort()
        {
            Transaction transaction1 = new Transaction(100);
            Assert.AreEqual("Active", transaction1.Status, "Initial status should be Active.");
            transaction1.Commit();
            Assert.AreEqual("Committed", transaction1.Status, "Status should be Committed after commit.");
            Transaction transaction2 = new Transaction(101);
            Assert.AreEqual("Active", transaction2.Status, "Initial status should be Active.");
            transaction2.Abort();
            Assert.AreEqual("Aborted", transaction2.Status, "Status should be Aborted after abort.");
        }

        [TestMethod]
        public void Test_LogManager_Synthetic_LargeNumberOfLogs()
        {
            LogManager logManager = new LogManager();
            int totalLogs = 1000;
            for (int i = 0; i < totalLogs; i++)
            {
                LogRecord record = new LogRecord(i, "Synthetic Operation " + i, DateTime.UtcNow);
                record.RecordType = LogType.Update;
                record.PageId = (i % 50) + 1;
                logManager.WriteLog(record);
            }

            List<LogRecord> logs = logManager.ReadLog();
            Assert.AreEqual(totalLogs, logs.Count, "Log count should match the number of synthetic logs written.");
            long previousLSN = 0;
            for (int i = 0; i < logs.Count; i++)
            {
                Assert.IsTrue(logs[i].LSN > previousLSN, "LSN should be in increasing order.");
                previousLSN = logs[i].LSN;
            }
        }
    }
}