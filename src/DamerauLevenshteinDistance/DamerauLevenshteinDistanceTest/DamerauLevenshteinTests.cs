using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DamerauLevenshteinDistance;

namespace DamerauLevenshteinDistanceTest
{
    [TestClass]
    public class DamerauLevenshteinTests
    {
        [TestMethod]
        public void TestIdenticalStrings()
        {
            int distance = DamerauLevenshtein.ComputeDistance("abc", "abc");
            Assert.AreEqual(0, distance);
        }

        [TestMethod]
        public void TestSingleSubstitution()
        {
            int distance = DamerauLevenshtein.ComputeDistance("abc", "abd");
            Assert.AreEqual(1, distance);
        }

        [TestMethod]
        public void TestInsertionDeletion()
        {
            int distance = DamerauLevenshtein.ComputeDistance("kitten", "sitting");
            Assert.AreEqual(3, distance);
        }

        [TestMethod]
        public void TestTransposition()
        {
            int distance = DamerauLevenshtein.ComputeDistance("abcd", "abdc");
            Assert.AreEqual(1, distance);
        }

        [TestMethod]
        public void TestNullInputs()
        {
            int distance1 = DamerauLevenshtein.ComputeDistance(null, "abc");
            Assert.AreEqual(3, distance1);
            int distance2 = DamerauLevenshtein.ComputeDistance("abc", null);
            Assert.AreEqual(3, distance2);
            int distance3 = DamerauLevenshtein.ComputeDistance(null, null);
            Assert.AreEqual(0, distance3);
        }

        [TestMethod]
        public void TestSymmetryAndRandomData()
        {
            Random random = new Random(12345);
            for (int i = 0; i < 10; i++)
            {
                int length1 = random.Next(10, 51);
                int length2 = random.Next(10, 51);
                string s1 = GenerateRandomString(length1, random);
                string s2 = GenerateRandomString(length2, random);
                int distance1 = DamerauLevenshtein.ComputeDistance(s1, s2);
                int distance2 = DamerauLevenshtein.ComputeDistance(s2, s1);
                Assert.AreEqual(distance1, distance2, "Distance should be symmetric.");
                Assert.IsTrue(distance1 >= 0, "Distance should be non-negative.");
            }
        }

        [TestMethod]
        public void TestLargeRandomData()
        {
            Random random = new Random(54321);
            int length1 = 1000;
            int length2 = 1000;
            string s1 = GenerateRandomString(length1, random);
            string s2 = GenerateRandomString(length2, random);
            int distance1 = DamerauLevenshtein.ComputeDistance(s1, s2);
            int distance2 = DamerauLevenshtein.ComputeDistance(s2, s1);
            Assert.AreEqual(distance1, distance2, "Distance should be symmetric for large strings.");
            Assert.IsTrue(distance1 >= 0, "Distance should be non-negative for large strings.");
        }

        private static string GenerateRandomString(int length, Random random)
        {
            char[] characters = new char[length];
            for (int i = 0; i < length; i++)
            {
                characters[i] = (char)random.Next(97, 123);
            }

            return new string (characters);
        }
    }
}