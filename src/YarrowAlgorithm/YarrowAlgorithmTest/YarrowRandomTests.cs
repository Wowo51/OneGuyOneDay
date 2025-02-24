using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YarrowAlgorithm;

namespace YarrowAlgorithmTest
{
    [TestClass]
    public class YarrowRandomTests
    {
        [TestMethod]
        public void TestDefaultConstructor_AndNextBytes()
        {
            YarrowRandom randomGenerator = new YarrowRandom();
            byte[] buffer = new byte[16];
            randomGenerator.NextBytes(buffer);
            bool hasNonZero = false;
            for (int index = 0; index < buffer.Length; index++)
            {
                if (buffer[index] != 0)
                {
                    hasNonZero = true;
                    break;
                }
            }

            Assert.IsTrue(hasNonZero, "NextBytes should fill the buffer with non-zero random data.");
        }

        [TestMethod]
        public void TestConstructorWithSeed_ProducesConsistentOutput()
        {
            byte[] seed = new byte[]
            {
                10,
                20,
                30,
                40,
                50,
                60,
                70,
                80,
                90,
                100,
                110,
                120,
                130,
                140,
                150,
                160
            };
            YarrowRandom random1 = new YarrowRandom(seed);
            YarrowRandom random2 = new YarrowRandom(seed);
            int output1 = random1.NextInt32();
            int output2 = random2.NextInt32();
            Assert.AreEqual(output1, output2, "Random generators initialized with the same seed should produce identical first outputs.");
        }

        [TestMethod]
        public void TestNextInt32_GeneratesDifferentValues()
        {
            YarrowRandom randomGenerator = new YarrowRandom();
            int firstValue = randomGenerator.NextInt32();
            int secondValue = randomGenerator.NextInt32();
            Assert.AreNotEqual(firstValue, secondValue, "Consecutive calls to NextInt32 should produce different values.");
        }

        [TestMethod]
        public void TestNextBytes_NullBufferHandled()
        {
            YarrowRandom randomGenerator = new YarrowRandom();
            try
            {
                randomGenerator.NextBytes(null);
            }
            catch (Exception exception)
            {
                Assert.Fail("NextBytes should handle null buffer without exception, but got: " + exception.Message);
            }
        }

        [TestMethod]
        public void TestReseed_Functionality()
        {
            YarrowRandom randomGenerator = new YarrowRandom();
            int beforeReseed = randomGenerator.NextInt32();
            byte[] newSeed = new byte[]
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
            randomGenerator.Reseed(newSeed);
            // Create a new instance with the same seed to compare first output.
            YarrowRandom newRandom = new YarrowRandom(newSeed);
            int afterReseed = randomGenerator.NextInt32();
            int expectedOutput = newRandom.NextInt32();
            Assert.AreEqual(expectedOutput, afterReseed, "After reseeding with the same seed, the generator should produce consistent output.");
        }
    }
}