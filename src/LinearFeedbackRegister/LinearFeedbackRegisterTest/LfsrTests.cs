using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinearFeedbackRegister;

namespace LinearFeedbackRegisterTest
{
    [TestClass]
    public class LfsrTests
    {
        [TestMethod]
        public void TestSeedZero()
        {
            LinearFeedbackRegister.Lfsr lfsr = new LinearFeedbackRegister.Lfsr(0, 0xB400);
            int firstValue = lfsr.Next();
            Assert.AreEqual(0, firstValue, "With seed 0, expected first Next() to be 0.");
        }

        [TestMethod]
        public void TestKnownSequence()
        {
            // For seed = 0xACE1 and tap mask = 0xB400:
            // Calculation:
            //   Initial state = 0xACE1.
            //   state & tapMask = 0xACE1 & 0xB400 = 0xA400, whose parity is odd (3 ones),
            //   so feedback = 1.
            //   Then next state = (0xACE1 >> 1) | (1 << 31)
            //               = (22128) | (2147483648)
            //               = 0x80005670, which as a signed int is -2147461520.
            LinearFeedbackRegister.Lfsr lfsr = new LinearFeedbackRegister.Lfsr(0xACE1, 0xB400);
            int firstValue = lfsr.Next();
            Assert.AreEqual(-2147461520, firstValue, "The first output from Lfsr did not match expected value.");
        }

        [TestMethod]
        public void TestRepeatability()
        {
            LinearFeedbackRegister.Lfsr lfsr1 = new LinearFeedbackRegister.Lfsr(0xACE1, 0xB400);
            LinearFeedbackRegister.Lfsr lfsr2 = new LinearFeedbackRegister.Lfsr(0xACE1, 0xB400);
            int i;
            for (i = 0; i < 50; i++)
            {
                int value1 = lfsr1.Next();
                int value2 = lfsr2.Next();
                Assert.AreEqual(value1, value2, "Sequence values differ at iteration " + i.ToString());
            }
        }

        [TestMethod]
        public void TestRandomizedSequence()
        {
            System.Random random = new System.Random(12345);
            int testIndex;
            for (testIndex = 0; testIndex < 5; testIndex++)
            {
                int seed = random.Next();
                int tapMask = random.Next(1, Int32.MaxValue); // ensure tapMask is non-zero
                LinearFeedbackRegister.Lfsr lfsr = new LinearFeedbackRegister.Lfsr(seed, tapMask);
                int firstVal = lfsr.Next();
                bool changed = false;
                int i;
                for (i = 1; i < 100; i++)
                {
                    int nextVal = lfsr.Next();
                    if (nextVal != firstVal)
                    {
                        changed = true;
                        break;
                    }
                }

                Assert.IsTrue(changed, "Randomized Lfsr did not produce a different value within 100 iterations.");
            }
        }
    }
}