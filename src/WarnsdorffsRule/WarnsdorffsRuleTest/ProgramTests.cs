using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WarnsdorffsRule;

namespace WarnsdorffsRuleTest
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void Test_Main_Output()
        {
            StringWriter sw = new StringWriter();
            TextWriter originalOut = Console.Out;
            Console.SetOut(sw);
            Program.Main(new string[0]);
            Console.SetOut(originalOut);
            string output = sw.ToString().Trim();
            Assert.IsFalse(string.IsNullOrEmpty(output), "Program output should not be empty.");
            if (output.StartsWith("No complete tour"))
            {
                Assert.AreEqual("No complete tour found.", output);
            }
            else
            {
                string[] lines = output.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                Assert.AreEqual(64, lines.Length, "A complete tour should contain exactly 64 moves.");
                foreach (string line in lines)
                {
                    Assert.IsTrue(line.StartsWith("(") && line.EndsWith(")"), "Output format for coordinate is invalid: " + line);
                }
            }
        }
    }
}