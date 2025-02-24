using Microsoft.VisualStudio.TestTools.UnitTesting;
using HuangAlgorithm;

namespace HuangAlgorithmTest
{
    [TestClass]
    public class HuangAlgorithmTests
    {
        [TestMethod]
        public void TestSendMessage()
        {
            DistributedProcess sender = new DistributedProcess("Sender");
            DistributedProcess receiver = new DistributedProcess("Receiver");
            sender.SendMessage(receiver);
            Assert.AreEqual(1, sender.SentMessages);
            Assert.AreEqual(1, receiver.ReceivedMessages);
            Assert.IsTrue(receiver.IsActive);
        }

        [TestMethod]
        public void TestCompleteWork()
        {
            DistributedProcess process = new DistributedProcess("P");
            process.IsActive = true;
            process.CompleteWork();
            Assert.IsFalse(process.IsActive);
        }

        [TestMethod]
        public void TestCheckTerminationTrue()
        {
            HuangTerminationDetector detector = new HuangTerminationDetector();
            DistributedProcess p1 = new DistributedProcess("P1")
            {
                SentMessages = 1,
                ReceivedMessages = 0,
                IsActive = false
            };
            DistributedProcess p2 = new DistributedProcess("P2")
            {
                SentMessages = 1,
                ReceivedMessages = 1,
                IsActive = false
            };
            DistributedProcess p3 = new DistributedProcess("P3")
            {
                SentMessages = 0,
                ReceivedMessages = 1,
                IsActive = false
            };
            detector.AddProcess(p1);
            detector.AddProcess(p2);
            detector.AddProcess(p3);
            bool termination = detector.CheckTermination();
            Assert.IsTrue(termination);
        }

        [TestMethod]
        public void TestCheckTerminationActive()
        {
            HuangTerminationDetector detector = new HuangTerminationDetector();
            DistributedProcess p1 = new DistributedProcess("P1")
            {
                SentMessages = 1,
                ReceivedMessages = 0,
                IsActive = true
            };
            DistributedProcess p2 = new DistributedProcess("P2")
            {
                SentMessages = 1,
                ReceivedMessages = 1,
                IsActive = false
            };
            DistributedProcess p3 = new DistributedProcess("P3")
            {
                SentMessages = 0,
                ReceivedMessages = 1,
                IsActive = false
            };
            detector.AddProcess(p1);
            detector.AddProcess(p2);
            detector.AddProcess(p3);
            bool termination = detector.CheckTermination();
            Assert.IsFalse(termination);
        }

        [TestMethod]
        public void TestCheckTerminationNonZeroDifference()
        {
            HuangTerminationDetector detector = new HuangTerminationDetector();
            DistributedProcess p1 = new DistributedProcess("P1")
            {
                SentMessages = 2,
                ReceivedMessages = 0,
                IsActive = false
            };
            DistributedProcess p2 = new DistributedProcess("P2")
            {
                SentMessages = 1,
                ReceivedMessages = 1,
                IsActive = false
            };
            DistributedProcess p3 = new DistributedProcess("P3")
            {
                SentMessages = 0,
                ReceivedMessages = 1,
                IsActive = false
            };
            detector.AddProcess(p1);
            detector.AddProcess(p2);
            detector.AddProcess(p3);
            bool termination = detector.CheckTermination();
            Assert.IsFalse(termination);
        }

        [TestMethod]
        public void TestRunSimulation()
        {
            HuangTerminationDetector detector = new HuangTerminationDetector();
            DistributedProcess p1 = new DistributedProcess("P1");
            DistributedProcess p2 = new DistributedProcess("P2");
            DistributedProcess p3 = new DistributedProcess("P3");
            detector.AddProcess(p1);
            detector.AddProcess(p2);
            detector.AddProcess(p3);
            detector.RunSimulation();
            Assert.AreEqual(1, p1.SentMessages);
            Assert.AreEqual(0, p1.ReceivedMessages);
            Assert.IsFalse(p1.IsActive);
            Assert.AreEqual(1, p2.SentMessages);
            Assert.AreEqual(1, p2.ReceivedMessages);
            Assert.IsFalse(p2.IsActive);
            Assert.AreEqual(0, p3.SentMessages);
            Assert.AreEqual(1, p3.ReceivedMessages);
            Assert.IsFalse(p3.IsActive);
            bool termination = detector.CheckTermination();
            Assert.IsTrue(termination);
        }

        [TestMethod]
        public void TestLargeDataTermination()
        {
            HuangTerminationDetector detector = new HuangTerminationDetector();
            for (int i = 0; i < 100; i++)
            {
                DistributedProcess process = new DistributedProcess("P" + i.ToString())
                {
                    SentMessages = 10,
                    ReceivedMessages = 10,
                    IsActive = false
                };
                detector.AddProcess(process);
            }

            bool termination = detector.CheckTermination();
            Assert.IsTrue(termination);
        }
    }
}