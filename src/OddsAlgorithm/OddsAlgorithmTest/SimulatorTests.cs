using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OddsAlgorithm;

namespace OddsAlgorithmTest
{
    [TestClass]
    public class SimulatorTests
    {
        [TestMethod]
        public void RunSimulation_PrintsExpectedOutput()
        {
            Simulator simulator = new Simulator();
            StringWriter stringWriter = new StringWriter();
            TextWriter originalOut = Console.Out;
            try
            {
                Console.SetOut(stringWriter);
                simulator.RunSimulation();
                string output = stringWriter.ToString();
                Assert.IsTrue(output.Contains("Outcomes:"), "Output should contain 'Outcomes:'");
                Assert.IsTrue(output.Contains("Prediction"), "Output should contain 'Prediction'");
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }
    }
}