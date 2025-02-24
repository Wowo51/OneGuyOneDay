using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CristianAlgorithmSync;

namespace CristianAlgorithmSyncTest
{
    [TestClass]
    public class TimeServerTests
    {
        [TestMethod]
        public void Test_GetServerTime_ReturnsRecentTime()
        {
            TimeServer timeServer = new TimeServer();
            DateTime before = DateTime.UtcNow;
            DateTime serverTime = timeServer.GetServerTime();
            DateTime after = DateTime.UtcNow;
            Assert.IsTrue(serverTime >= before && serverTime <= after, "TimeServer did not return a time between before and after.");
        }
    }
}