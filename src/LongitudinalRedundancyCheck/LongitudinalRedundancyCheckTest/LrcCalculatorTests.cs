using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Globalization;
using LongitudinalRedundancyCheck;

namespace LongitudinalRedundancyCheckTest
{
    [TestClass]
    public class LrcCalculatorTests
    {
        public TestContext TestContext { get; set; }

        private static readonly object ConsoleLock = new object ();
        [TestInitialize]
        public void TestSetup()
        {
            TestContext.WriteLine("Starting test: " + TestContext.TestName);
        }

        [TestCleanup]
        public void TestTearDown()
        {
            TestContext.WriteLine("Finished test: " + TestContext.TestName);
        }

        [TestMethod]
        public void TestComputeLRC_NullInput()
        {
            byte result = LongitudinalRedundancyCheck.LrcCalculator.ComputeLRC(null);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void TestComputeLRC_EmptyArray()
        {
            byte[] data = new byte[0];
            byte result = LongitudinalRedundancyCheck.LrcCalculator.ComputeLRC(data);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void TestComputeLRC_KnownValue()
        {
            byte[] data = new byte[]
            {
                0x01,
                0x02,
                0x03
            };
            // Sum: 1+2+3 = 6; expected LRC = (256 - 6) & 0xFF = 250 (0xFA)
            byte expected = 250;
            byte result = LongitudinalRedundancyCheck.LrcCalculator.ComputeLRC(data);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestComputeLRC_LargeData()
        {
            int length = 1000;
            byte[] data = new byte[length];
            Random random = new Random(12345);
            for (int index = 0; index < length; index++)
            {
                data[index] = (byte)random.Next(0, 256);
            }

            int sum = 0;
            for (int index = 0; index < length; index++)
            {
                sum = (sum + data[index]) & 0xFF;
            }

            byte expected = (byte)((256 - sum) & 0xFF);
            byte result = LongitudinalRedundancyCheck.LrcCalculator.ComputeLRC(data);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestProgram_NoArguments()
        {
            lock (ConsoleLock)
            {
                StringWriter writer = new StringWriter();
                TextWriter originalOut = Console.Out;
                TextWriter originalError = Console.Error;
                Console.SetOut(writer);
                Console.SetError(writer);
                try
                {
                    string[] args = new string[0];
                    int exitCode = LongitudinalRedundancyCheck.Program.Main(args);
                }
                finally
                {
                    Console.SetOut(originalOut);
                    Console.SetError(originalError);
                }

                string output = writer.ToString().Trim();
                Assert.AreEqual("Provide hex values separated by space.", output);
            }
        }

        [TestMethod]
        public void TestProgram_InvalidInput()
        {
            lock (ConsoleLock)
            {
                StringWriter writer = new StringWriter();
                TextWriter originalOut = Console.Out;
                TextWriter originalError = Console.Error;
                Console.SetOut(writer);
                Console.SetError(writer);
                try
                {
                    string[] args = new string[]
                    {
                        "ZZ"
                    };
                    int exitCode = LongitudinalRedundancyCheck.Program.Main(args);
                }
                finally
                {
                    Console.SetOut(originalOut);
                    Console.SetError(originalError);
                }

                string output = writer.ToString().Trim();
                Assert.IsTrue(output.StartsWith("Invalid input:"));
            }
        }

        [TestMethod]
        public void TestProgram_ValidInput()
        {
            lock (ConsoleLock)
            {
                StringWriter writer = new StringWriter();
                TextWriter originalOut = Console.Out;
                TextWriter originalError = Console.Error;
                Console.SetOut(writer);
                Console.SetError(writer);
                try
                {
                    string[] args = new string[]
                    {
                        "01",
                        "02",
                        "03"
                    };
                    int exitCode = LongitudinalRedundancyCheck.Program.Main(args);
                }
                finally
                {
                    Console.SetOut(originalOut);
                    Console.SetError(originalError);
                }

                string output = writer.ToString().Trim();
                Assert.IsTrue(output.Contains("LRC:"));
                Assert.IsTrue(output.Contains("FA") || output.Contains("fa"));
            }
        }
    }
}