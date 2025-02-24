using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RadixSort;

namespace RadixSortTest
{
    [TestClass]
    public class RadixSortTests
    {
        [TestMethod]
        public void Test_NullInput_ReturnsEmptyArray()
        {
            string? []? input = null;
            string[] actual = StringRadixSorter.Sort(input);
            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.Length);
        }

        [TestMethod]
        public void Test_EmptyArray_ReturnsEmptyArray()
        {
            string[] input = new string[0];
            string[] actual = StringRadixSorter.Sort(input);
            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.Length);
        }

        [TestMethod]
        public void Test_BasicSort()
        {
            string[] input = new string[]
            {
                "banana",
                "apple",
                "cherry"
            };
            string[] expected = new string[]
            {
                "apple",
                "banana",
                "cherry"
            };
            string[] actual = StringRadixSorter.Sort(input);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_StringsWithNullElements()
        {
            string? [] input = new string? []
            {
                "dog",
                null,
                "cat"
            };
            // null values are treated as empty strings.
            string[] expected = new string[]
            {
                "",
                "cat",
                "dog"
            };
            string[] actual = StringRadixSorter.Sort(input);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_LargeRandomData()
        {
            const int size = 1000;
            Random random = new Random(42);
            string? [] input = new string? [size];
            for (int i = 0; i < size; i++)
            {
                input[i] = GenerateRandomString(random, random.Next(0, 21));
            }

            string[] expected = new string[size];
            Array.Copy(input, expected, size);
            Array.Sort(expected, StringComparer.Ordinal);
            string[] actual = StringRadixSorter.Sort(input);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_DuplicateStrings()
        {
            string? [] input = new string? []
            {
                "zebra",
                "apple",
                "apple",
                "banana",
                "zebra"
            };
            string[] expected = new string[]
            {
                "apple",
                "apple",
                "banana",
                "zebra",
                "zebra"
            };
            string[] actual = StringRadixSorter.Sort(input);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_SpecialCharacters()
        {
            string[] input = new string[]
            {
                "b#d",
                "a-c",
                "a b",
                "z!y",
                "abc"
            };
            string[] expected = new string[]
            {
                "a b",
                "a-c",
                "abc",
                "b#d",
                "z!y"
            };
            string[] actual = StringRadixSorter.Sort(input);
            CollectionAssert.AreEqual(expected, actual);
        }

        private string GenerateRandomString(Random random, int length)
        {
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                // Generate a random lowercase letter between a and z.
                chars[i] = (char)random.Next(97, 123);
            }

            return new string (chars);
        }
    }
}