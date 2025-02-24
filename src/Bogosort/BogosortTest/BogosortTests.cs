using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bogosort;

namespace BogosortTest
{
    [TestClass]
    public class BogosortTests
    {
        [TestMethod]
        public void TestSort_NullArray()
        {
            int[] array = null;
            BogosortAlgorithm.Sort<int>(array);
        }

        [TestMethod]
        public void TestSort_SingleElement()
        {
            int[] array =
            {
                1
            };
            BogosortAlgorithm.Sort<int>(array);
            CollectionAssert.AreEqual(new int[] { 1 }, array);
        }

        [TestMethod]
        [Timeout(2000)]
        public void TestSort_AlreadySorted()
        {
            int[] array =
            {
                1,
                2,
                3,
                4,
                5
            };
            BogosortAlgorithm.Sort<int>(array);
            for (int i = 1; i < array.Length; i++)
            {
                Assert.IsTrue(array[i - 1] <= array[i]);
            }
        }

        [TestMethod]
        [Timeout(2000)]
        public void TestSort_Unsorted()
        {
            int[] array =
            {
                3,
                1,
                2
            };
            BogosortAlgorithm.Sort<int>(array);
            for (int i = 1; i < array.Length; i++)
            {
                Assert.IsTrue(array[i - 1] <= array[i]);
            }
        }

        [TestMethod]
        [Timeout(2000)]
        public void TestSort_WithDuplicates()
        {
            int[] array =
            {
                3,
                1,
                2,
                1
            };
            BogosortAlgorithm.Sort<int>(array);
            for (int i = 1; i < array.Length; i++)
            {
                Assert.IsTrue(array[i - 1] <= array[i]);
            }
        }

        [TestMethod]
        [Timeout(2000)]
        public void TestSort_Synthetic_Int()
        {
            System.Random random = new System.Random();
            for (int testIndex = 0; testIndex < 10; testIndex++)
            {
                int size = random.Next(2, 5);
                int[] array = new int[size];
                for (int i = 0; i < size; i++)
                {
                    array[i] = random.Next(0, 10);
                }

                int[] original = (int[])array.Clone();
                BogosortAlgorithm.Sort<int>(array);
                for (int i = 1; i < array.Length; i++)
                {
                    Assert.IsTrue(array[i - 1] <= array[i], "Original: " + string.Join(",", original) + " Sorted: " + string.Join(",", array));
                }
            }
        }

        [TestMethod]
        [Timeout(2000)]
        public void TestSort_StringArray()
        {
            string[] array =
            {
                "banana",
                "apple",
                "cherry"
            };
            BogosortAlgorithm.Sort<string>(array);
            for (int i = 1; i < array.Length; i++)
            {
                Assert.IsTrue(string.Compare(array[i - 1], array[i]) <= 0);
            }
        }

        [TestMethod]
        public void TestSort_EmptyArray()
        {
            int[] array = new int[0];
            BogosortAlgorithm.Sort(array);
            CollectionAssert.AreEqual(new int[0], array);
        }

        [TestMethod]
        [Timeout(2000)]
        public void TestSort_Synthetic_RandomStrings()
        {
            System.Random random = new System.Random();
            for (int testIndex = 0; testIndex < 10; testIndex++)
            {
                int size = random.Next(2, 5);
                string[] array = TestDataGenerator.GenerateRandomStringArray(size, 5);
                string[] original = (string[])array.Clone();
                BogosortAlgorithm.Sort<string>(array);
                for (int i = 1; i < array.Length; i++)
                {
                    Assert.IsTrue(string.Compare(array[i - 1], array[i]) <= 0, "Original: " + string.Join(",", original) + " Sorted: " + string.Join(",", array));
                }
            }
        }
    }
}