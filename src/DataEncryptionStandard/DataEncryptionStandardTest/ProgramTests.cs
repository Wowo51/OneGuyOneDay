using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataEncryptionStandard;

namespace DataEncryptionStandardTest
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void TestMain_Returns0AndCorrectOutput()
        {
            StringWriter stringWriter = new StringWriter();
            TextWriter originalOutput = Console.Out;
            try
            {
                Console.SetOut(stringWriter);
                int exitCode = Program.Main(new string[0]);
                Console.Out.Flush();
                string output = stringWriter.ToString();
                Assert.AreEqual(0, exitCode);
                Assert.IsTrue(output.Contains("Decrypted Message:"), "Output should contain 'Decrypted Message:'");
                Assert.IsTrue(output.Contains("Test Message"), "Output should include 'Test Message'");
            }
            finally
            {
                Console.SetOut(originalOutput);
            }
        }
    }
}