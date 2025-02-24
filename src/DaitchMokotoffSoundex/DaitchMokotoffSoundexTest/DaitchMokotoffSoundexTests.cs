using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DaitchMokotoffSoundex;

namespace DaitchMokotoffSoundexTest
{
    [TestClass]
    public class DaitchMokotoffSoundexTests
    {
        [TestMethod]
        public void Test_Encode_NullInput_ReturnsEmptyArray()
        {
            string[] codes = DaitchMokotoffSoundex.DaitchMokotoffSoundex.Encode(null);
            Assert.IsNotNull(codes);
            Assert.AreEqual(0, codes.Length);
        }

        [TestMethod]
        public void Test_Encode_SingularC_ReturnsTwoAlternatives()
        {
            string[] codes = DaitchMokotoffSoundex.DaitchMokotoffSoundex.Encode("C");
            Assert.AreEqual(2, codes.Length);
            CollectionAssert.Contains(codes, "400000");
            CollectionAssert.Contains(codes, "500000");
        }

        [TestMethod]
        public void Test_Encode_SCH_ReturnsExpectedAlternatives()
        {
            string[] codes = DaitchMokotoffSoundex.DaitchMokotoffSoundex.Encode("SCH");
            Assert.AreEqual(2, codes.Length);
            CollectionAssert.Contains(codes, "400000");
            CollectionAssert.Contains(codes, "500000");
        }

        [TestMethod]
        public void Test_Encode_DZ_ReturnsCorrectCode()
        {
            string[] codes = DaitchMokotoffSoundex.DaitchMokotoffSoundex.Encode("DZ");
            Assert.AreEqual(1, codes.Length);
            Assert.AreEqual("300000", codes[0]);
        }

        [TestMethod]
        public void Test_Encode_CH_ReturnsCorrectCode()
        {
            string[] codes = DaitchMokotoffSoundex.DaitchMokotoffSoundex.Encode("CH");
            Assert.AreEqual(1, codes.Length);
            Assert.AreEqual("500000", codes[0]);
        }

        [TestMethod]
        public void Test_Encode_R_ReturnsCorrectCode()
        {
            string[] codes = DaitchMokotoffSoundex.DaitchMokotoffSoundex.Encode("R");
            Assert.AreEqual(1, codes.Length);
            Assert.AreEqual("900000", codes[0]);
        }

        [TestMethod]
        public void Test_RandomNames_ProduceSixDigitCodes()
        {
            Random random = new Random();
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (int i = 0; i < 100; i++)
            {
                StringBuilder nameBuilder = new StringBuilder();
                int nameLength = random.Next(3, 10);
                for (int j = 0; j < nameLength; j++)
                {
                    int index = random.Next(0, letters.Length);
                    nameBuilder.Append(letters[index]);
                }

                string name = nameBuilder.ToString();
                string[] codes = DaitchMokotoffSoundex.DaitchMokotoffSoundex.Encode(name);
                foreach (string code in codes)
                {
                    Assert.AreEqual(6, code.Length, "Name: " + name + ", Code: " + code);
                }
            }
        }
    }
}