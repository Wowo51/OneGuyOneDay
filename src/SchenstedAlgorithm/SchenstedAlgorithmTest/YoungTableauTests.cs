using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchenstedAlgorithm;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchenstedAlgorithmTest
{
    [TestClass]
    public class YoungTableauTests
    {
        [TestMethod]
        public void TestToString_Empty()
        {
            YoungTableau tableau = new YoungTableau();
            string result = tableau.ToString();
            Assert.AreEqual(string.Empty, result, "Empty tableau should return an empty string.");
        }

        [TestMethod]
        public void TestToString_SingleRow()
        {
            YoungTableau tableau = new YoungTableau();
            List<int> row = new List<int>
            {
                1,
                2,
                3
            };
            tableau.Rows.Add(row);
            string result = tableau.ToString();
            string expected = "1 2 3" + Environment.NewLine;
            Assert.AreEqual(expected, result, "Single row tableau did not return expected string format.");
        }

        [TestMethod]
        public void TestToString_MultipleRows()
        {
            YoungTableau tableau = new YoungTableau();
            List<int> row1 = new List<int>
            {
                1,
                2,
                3
            };
            List<int> row2 = new List<int>
            {
                4,
                5
            };
            tableau.Rows.Add(row1);
            tableau.Rows.Add(row2);
            string result = tableau.ToString();
            string expected = "1 2 3" + Environment.NewLine + "4 5" + Environment.NewLine;
            Assert.AreEqual(expected, result, "Multiple row tableau did not return expected string format.");
        }

        [TestMethod]
        public void TestToString_EmptyRow()
        {
            YoungTableau tableau = new YoungTableau();
            List<int> emptyRow = new List<int>();
            tableau.Rows.Add(emptyRow);
            string result = tableau.ToString();
            string expected = Environment.NewLine;
            Assert.AreEqual(expected, result, "Empty row should return a newline.");
        }

        [TestMethod]
        public void TestToString_LargeRandomData()
        {
            YoungTableau tableau = new YoungTableau();
            Random random = new Random(42);
            int numRows = 50;
            StringBuilder expectedBuilder = new StringBuilder();
            for (int i = 0; i < numRows; i++)
            {
                int rowLength = random.Next(1, 10);
                List<int> row = new List<int>();
                List<string> rowStrings = new List<string>();
                for (int j = 0; j < rowLength; j++)
                {
                    int value = random.Next(0, 100);
                    row.Add(value);
                    rowStrings.Add(value.ToString());
                }

                tableau.Rows.Add(row);
                expectedBuilder.AppendLine(string.Join(" ", rowStrings));
            }

            string expected = expectedBuilder.ToString();
            string result = tableau.ToString();
            Assert.AreEqual(expected, result, "Large random data test failed.");
        }
    }
}