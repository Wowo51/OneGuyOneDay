using System;
using System.IO;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LamportsBakeryAlgorithm;

namespace LamportsBakeryAlgorithmTest
{
    [TestClass]
    public class BakeryLockTests
    {
        [TestMethod]
        public void TestSingleThreadLock()
        {
            int processCount = 1;
            BakeryLock lockInstance = new BakeryLock(processCount);
            int counter = 0;
            int iterations = 1000;
            for (int i = 0; i < iterations; i++)
            {
                lockInstance.Lock(0);
                counter = counter + 1;
                lockInstance.Unlock(0);
            }

            Assert.AreEqual(iterations, counter);
        }

        [TestMethod]
        public void TestConcurrentIncrement()
        {
            int threadCount = 10;
            int iterations = 1000;
            BakeryLock lockInstance = new BakeryLock(threadCount);
            int sharedCounter = 0;
            Thread[] threads = new Thread[threadCount];
            for (int i = 0; i < threadCount; i++)
            {
                int processId = i;
                threads[i] = new Thread(() =>
                {
                    for (int j = 0; j < iterations; j++)
                    {
                        lockInstance.Lock(processId);
                        int temp = sharedCounter;
                        temp = temp + 1;
                        sharedCounter = temp;
                        lockInstance.Unlock(processId);
                    }
                });
                threads[i].Start();
            }

            for (int i = 0; i < threadCount; i++)
            {
                threads[i].Join();
            }

            Assert.AreEqual(threadCount * iterations, sharedCounter);
        }

        [TestMethod]
        public void TestProgramMainOutput()
        {
            StringWriter output = new StringWriter();
            Console.SetOut(output);
            Program.Main(new string[0]);
            StringReader reader = new StringReader(output.ToString().Trim());
            string? line = reader.ReadLine();
            Assert.IsNotNull(line);
            Assert.AreEqual("Final count: 50", line);
        }

        [TestMethod]
        public void TestScalabilityRandomData()
        {
            Random random = new Random(42);
            int threadCount = random.Next(5, 15);
            int iterations = random.Next(500, 1000);
            BakeryLock lockInstance = new BakeryLock(threadCount);
            int sharedCounter = 0;
            Thread[] threads = new Thread[threadCount];
            for (int i = 0; i < threadCount; i++)
            {
                int processId = i;
                threads[i] = new Thread(() =>
                {
                    for (int j = 0; j < iterations; j++)
                    {
                        lockInstance.Lock(processId);
                        int temp = sharedCounter;
                        temp = temp + 1;
                        sharedCounter = temp;
                        lockInstance.Unlock(processId);
                    }
                });
                threads[i].Start();
            }

            for (int i = 0; i < threadCount; i++)
            {
                threads[i].Join();
            }

            Assert.AreEqual(threadCount * iterations, sharedCounter);
        }
    }
}