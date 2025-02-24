using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using PetricksMethod;

namespace PetricksMethodTest
{
    [TestClass]
    public class ProgramTests
    {
        public TestContext TestContext { get; set; } = null !;

        [TestMethod]
        public void TestMainOutputAndExitCode()
        {
            TestContext.WriteLine("Running TestMainOutputAndExitCode");
            StringWriter stringWriter = new StringWriter();
            TextWriter originalOut = Console.Out;
            try
            {
                Console.SetOut(stringWriter);
                int exitCode = Program.Main(new string[0]);
                string output = stringWriter.ToString();
                Assert.AreEqual(0, exitCode, "Program.Main should return exit code 0.");
                Assert.IsTrue(output.Contains("Optimal solutions:"), "Output should contain 'Optimal solutions:'.");
                string[] lines = output.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                Assert.IsTrue(lines.Length > 1, "Output should contain at least one solution line after the header.");
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }
    }
}