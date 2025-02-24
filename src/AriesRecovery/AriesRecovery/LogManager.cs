using System;
using System.Collections.Generic;

namespace AriesRecovery
{
    public class LogManager
    {
        private List<LogRecord> _logRecords;
        private static long _nextLSN = 1;
        public LogManager()
        {
            _logRecords = new List<LogRecord>();
        }

        public void WriteLog(LogRecord record)
        {
            if (record != null)
            {
                if (record.LSN == 0)
                {
                    record.LSN = _nextLSN;
                    _nextLSN++;
                }

                _logRecords.Add(record);
                Console.WriteLine("Log written: " + record.Operation + " with LSN: " + record.LSN);
            }
        }

        public List<LogRecord> ReadLog()
        {
            return new List<LogRecord>(_logRecords);
        }
    }
}