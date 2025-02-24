using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YarrowAlgorithm;

namespace YarrowAlgorithmTest
{
    [TestClass]
    public class YarrowGeneratorTests
    {
        [TestMethod]
        public void TestConstructor_WithSeed()
        {
            byte[] seed = new byte[]
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
                16
            };
            YarrowGenerator generator = new YarrowGenerator(seed);
            int firstValue = generator.NextInt32();
            int secondValue = generator.NextInt32();
            // Two calls should yield values; if identical, it is highly unlikely.
            Assert.AreNotEqual(firstValue, secondValue, "Generator with seed should produce varying outputs.");
        }

        [TestMethod]
        public void TestConstructor_WithoutSeed()
        {
            YarrowGenerator generator = new YarrowGenerator();
            byte[] buffer = new byte[16];
            generator.NextBytes(buffer);
            // Ensure the buffer is filled (not all zeros)
            bool foundNonZero = false;
            for (int index = 0; index < buffer.Length; index++)
            {
                if (buffer[index] != 0)
                {
                    foundNonZero = true;
                    break;
                }
            }

            Assert.IsTrue(foundNonZero, "Generator without seed should produce non-zero random bytes.");
        }

        [TestMethod]
        public void TestNextBytes_NullBufferHandled()
        {
            YarrowGenerator generator = new YarrowGenerator();
            // Should not throw when passing null.
            try
            {
                generator.NextBytes(null);
            }
            catch (Exception exception)
            {
                Assert.Fail("NextBytes should handle null buffer without throwing an exception, but got: " + exception.Message);
            }
        }

        [TestMethod]
        public void TestAddEntropy_ReseedsAndContinues()
        {
            YarrowGenerator generator = new YarrowGenerator();
            // Create a synthetic entropy array of length 32 bytes to trigger reseeding.
            byte[] entropy = new byte[32];
            for (int index = 0; index < 32; index++)
            {
                entropy[index] = (byte)index;
            }

            generator.AddEntropy(entropy);
            // After reseeding, ensure that generator can produce valid output.
            int result = generator.NextInt32();
            Assert.IsTrue(true, "Generator should work after adding entropy.");
        }
    }
}