using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading;
using PetersonAlgorithm;

namespace PetersonAlgorithmTest
{
    [TestClass]
    public class PetersonAlgorithmTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestProgramMainOutput()
        {
            TestContext.WriteLine("Starting TestProgramMainOutput");
            StringWriter outputWriter = new StringWriter();
            TextWriter originalConsoleOut = Console.Out;
            try
            {
                Console.SetOut(outputWriter);
                Program.Main(new string[0]);
                string output = outputWriter.ToString().Trim();
                TestContext.WriteLine("Captured output: " + output);
                Assert.IsTrue(output.Contains("Final counter: 2000"), "The final counter output did not match expected value.");
            }
            finally
            {
                Console.SetOut(originalConsoleOut);
            }

            TestContext.WriteLine("Finished TestProgramMainOutput");
        }

        [TestMethod]
        public void TestPetersonLockConcurrency()
        {
            TestContext.WriteLine("Starting TestPetersonLockConcurrency");
            PetersonLock petersonLock = new PetersonLock();
            int sharedCounter = 0;
            int iterations = 1000;
            Thread thread0 = new Thread(() =>
            {
                for (int i = 0; i < iterations; i++)
                {
                    petersonLock.Lock(0);
                    int temp = sharedCounter;
                    temp = temp + 1;
                    sharedCounter = temp;
                    petersonLock.Unlock(0);
                }
            });
            Thread thread1 = new Thread(() =>
            {
                for (int i = 0; i < iterations; i++)
                {
                    petersonLock.Lock(1);
                    int temp = sharedCounter;
                    temp = temp + 1;
                    sharedCounter = temp;
                    petersonLock.Unlock(1);
                }
            });
            thread0.Start();
            thread1.Start();
            thread0.Join();
            thread1.Join();
            TestContext.WriteLine("Shared counter value: " + sharedCounter.ToString());
            Assert.AreEqual(iterations * 2, sharedCounter, "Shared counter did not reach expected value under concurrency.");
            TestContext.WriteLine("Finished TestPetersonLockConcurrency");
        }

        [TestMethod]
        public void TestPetersonLockConcurrencyHighLoad()
        {
            TestContext.WriteLine("Starting TestPetersonLockConcurrencyHighLoad");
            PetersonLock petersonLock = new PetersonLock();
            int sharedCounter = 0;
            int iterations = 50000;
            Thread thread0 = new Thread(() =>
            {
                for (int i = 0; i < iterations; i++)
                {
                    petersonLock.Lock(0);
                    int temp = sharedCounter;
                    temp = temp + 1;
                    sharedCounter = temp;
                    petersonLock.Unlock(0);
                }
            });
            Thread thread1 = new Thread(() =>
            {
                for (int i = 0; i < iterations; i++)
                {
                    petersonLock.Lock(1);
                    int temp = sharedCounter;
                    temp = temp + 1;
                    sharedCounter = temp;
                    petersonLock.Unlock(1);
                }
            });
            thread0.Start();
            thread1.Start();
            thread0.Join();
            thread1.Join();
            TestContext.WriteLine("Shared counter value under high load: " + sharedCounter.ToString());
            Assert.AreEqual(iterations * 2, sharedCounter, "High load shared counter did not reach expected value under concurrency.");
            TestContext.WriteLine("Finished TestPetersonLockConcurrencyHighLoad");
        }

        [TestMethod]
        public void TestInvalidProcessIdDoesNothing()
        {
            TestContext.WriteLine("Starting TestInvalidProcessIdDoesNothing");
            PetersonLock petersonLock = new PetersonLock();
            int sharedCounter = 0;
            // Call lock/unlock with an invalid process id; these should not block or modify shared state.
            petersonLock.Lock(2);
            petersonLock.Unlock(2);
            // Perform a valid operation to ensure the lock still functions.
            petersonLock.Lock(0);
            sharedCounter++;
            petersonLock.Unlock(0);
            TestContext.WriteLine("Shared counter after valid operation: " + sharedCounter.ToString());
            Assert.AreEqual(1, sharedCounter, "Invalid process id operations affected the lock functionality.");
            TestContext.WriteLine("Finished TestInvalidProcessIdDoesNothing");
        }
    }
}