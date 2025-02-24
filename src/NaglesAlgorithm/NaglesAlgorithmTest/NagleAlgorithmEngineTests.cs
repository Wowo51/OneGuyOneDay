using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using NaglesAlgorithm;

namespace NaglesAlgorithmTest
{
    [TestClass]
    public class NagleAlgorithmEngineTests
    {
        private class FakeNetworkSender : INetworkSender
        {
            public List<string> SentPackets = new List<string>();
            public void Send(string data)
            {
                SentPackets.Add(data);
            }
        }

        [TestMethod]
        public void Test_QueuePacket_DoesNotFlush_WhenBufferNotExceeded()
        {
            FakeNetworkSender fakeSender = new FakeNetworkSender();
            NagleAlgorithmEngine engine = new NagleAlgorithmEngine(10, fakeSender);
            engine.QueuePacket("ab");
            engine.QueuePacket("cd");
            Assert.AreEqual(0, fakeSender.SentPackets.Count);
        }

        [TestMethod]
        public void Test_QueuePacket_AutoFlush_WhenBufferExceeded()
        {
            FakeNetworkSender fakeSender = new FakeNetworkSender();
            NagleAlgorithmEngine engine = new NagleAlgorithmEngine(10, fakeSender);
            engine.QueuePacket("abc"); // length 3
            engine.QueuePacket("def"); // length 3, total becomes 6
            engine.QueuePacket("ghij"); // length 4, total becomes 10 -> triggers flush
            Assert.AreEqual(1, fakeSender.SentPackets.Count);
            Assert.AreEqual("abcdefghij", fakeSender.SentPackets[0]);
        }

        [TestMethod]
        public void Test_FlushBuffer_ManuallyFlushesBuffer()
        {
            FakeNetworkSender fakeSender = new FakeNetworkSender();
            NagleAlgorithmEngine engine = new NagleAlgorithmEngine(100, fakeSender);
            engine.QueuePacket("hello");
            engine.QueuePacket("world");
            engine.FlushBuffer();
            Assert.AreEqual(1, fakeSender.SentPackets.Count);
            Assert.AreEqual("helloworld", fakeSender.SentPackets[0]);
        }

        [TestMethod]
        public void Test_QueuePacket_IgnoresNullOrEmpty()
        {
            FakeNetworkSender fakeSender = new FakeNetworkSender();
            NagleAlgorithmEngine engine = new NagleAlgorithmEngine(10, fakeSender);
            engine.QueuePacket(null);
            engine.QueuePacket("");
            engine.FlushBuffer();
            Assert.AreEqual(0, fakeSender.SentPackets.Count);
        }

        [TestMethod]
        public void Test_QueuePacket_MultipleAutoFlushes()
        {
            FakeNetworkSender fakeSender = new FakeNetworkSender();
            NagleAlgorithmEngine engine = new NagleAlgorithmEngine(5, fakeSender);
            engine.QueuePacket("ab"); // length = 2
            engine.QueuePacket("c"); // length = 1, cumulative = 3
            engine.QueuePacket("de"); // length = 2, cumulative becomes 5 -> triggers flush
            engine.QueuePacket("xy"); // length = 2
            engine.QueuePacket("z"); // length = 1, cumulative = 3
            engine.FlushBuffer();
            Assert.AreEqual(2, fakeSender.SentPackets.Count);
            Assert.AreEqual("abcde", fakeSender.SentPackets[0]);
            Assert.AreEqual("xyz", fakeSender.SentPackets[1]);
        }

        [TestMethod]
        public void Test_QueuePacket_LargeSyntheticData()
        {
            FakeNetworkSender fakeSender = new FakeNetworkSender();
            int maxBufferSize = 50;
            NagleAlgorithmEngine engine = new NagleAlgorithmEngine(maxBufferSize, fakeSender);
            List<string> packets = new List<string>();
            Random random = new Random(0);
            for (int i = 0; i < 100; i++)
            {
                int length = random.Next(1, 11);
                char[] chars = new char[length];
                for (int j = 0; j < length; j++)
                {
                    chars[j] = (char)('a' + random.Next(0, 26));
                }

                string packet = new string (chars);
                packets.Add(packet);
                engine.QueuePacket(packet);
            }

            engine.FlushBuffer();
            List<string> expectedFlushes = new List<string>();
            string currentGroup = "";
            foreach (string packet in packets)
            {
                currentGroup += packet;
                if (currentGroup.Length >= maxBufferSize)
                {
                    expectedFlushes.Add(currentGroup);
                    currentGroup = "";
                }
            }

            if (currentGroup.Length > 0)
            {
                expectedFlushes.Add(currentGroup);
            }

            Assert.AreEqual(expectedFlushes.Count, fakeSender.SentPackets.Count);
            for (int i = 0; i < expectedFlushes.Count; i++)
            {
                Assert.AreEqual(expectedFlushes[i], fakeSender.SentPackets[i]);
            }
        }
    }
}