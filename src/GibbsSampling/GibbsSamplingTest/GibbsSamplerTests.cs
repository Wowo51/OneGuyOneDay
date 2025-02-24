using Microsoft.VisualStudio.TestTools.UnitTesting;
using GibbsSampling;
using System.Collections.Generic;

namespace GibbsSamplingTest
{
    [TestClass]
    public class GibbsSamplerTests
    {
        [TestMethod]
        public void TestNextMethod_UpdateState()
        {
            double[] initialState = new double[]
            {
                10.0,
                20.0
            };
            List<GibbsSampler.ConditionalSampler> samplers = new List<GibbsSampler.ConditionalSampler>();
            GibbsSampler.ConditionalSampler sampler1 = delegate (double[] state)
            {
                return 5.0;
            };
            GibbsSampler.ConditionalSampler sampler2 = delegate (double[] state)
            {
                return 15.0;
            };
            samplers.Add(sampler1);
            samplers.Add(sampler2);
            GibbsSampler sampler = new GibbsSampler(initialState, samplers);
            double[] result = sampler.Next();
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual(5.0, result[0]);
            Assert.AreEqual(15.0, result[1]);
        }

        [TestMethod]
        public void TestRunMethod_CorrectIterations()
        {
            double[] initialState = new double[]
            {
                1.0,
                2.0,
                3.0
            };
            List<GibbsSampler.ConditionalSampler> samplers = new List<GibbsSampler.ConditionalSampler>();
            samplers.Add(delegate (double[] state)
            {
                return state[0] * 2;
            });
            samplers.Add(delegate (double[] state)
            {
                return state[1] * 2;
            });
            samplers.Add(delegate (double[] state)
            {
                return state[2] * 2;
            });
            GibbsSampler sampler = new GibbsSampler(initialState, samplers);
            List<double[]> samples = sampler.Run(2);
            Assert.AreEqual(2, samples.Count);
            double[] firstSample = samples[0];
            double[] secondSample = samples[1];
            Assert.AreEqual(2.0, firstSample[0]);
            Assert.AreEqual(4.0, firstSample[1]);
            Assert.AreEqual(6.0, firstSample[2]);
            Assert.AreEqual(4.0, secondSample[0]);
            Assert.AreEqual(8.0, secondSample[1]);
            Assert.AreEqual(12.0, secondSample[2]);
        }

        [TestMethod]
        public void TestMismatchedLength_InitialStateLonger()
        {
            double[] initialState = new double[]
            {
                1.0,
                2.0,
                3.0
            };
            List<GibbsSampler.ConditionalSampler> samplers = new List<GibbsSampler.ConditionalSampler>();
            samplers.Add(delegate (double[] state)
            {
                return 10.0;
            });
            samplers.Add(delegate (double[] state)
            {
                return 20.0;
            });
            GibbsSampler sampler = new GibbsSampler(initialState, samplers);
            double[] result = sampler.Next();
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual(10.0, result[0]);
            Assert.AreEqual(20.0, result[1]);
        }

        [TestMethod]
        public void TestMismatchedLength_SamplersLonger()
        {
            double[] initialState = new double[]
            {
                7.0
            };
            List<GibbsSampler.ConditionalSampler> samplers = new List<GibbsSampler.ConditionalSampler>();
            samplers.Add(delegate (double[] state)
            {
                return 100.0;
            });
            samplers.Add(delegate (double[] state)
            {
                return 200.0;
            });
            GibbsSampler sampler = new GibbsSampler(initialState, samplers);
            double[] result = sampler.Next();
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(100.0, result[0]);
        }

        [TestMethod]
        public void TestNullInitialState()
        {
            double[]? initialState = null;
            List<GibbsSampler.ConditionalSampler> samplers = new List<GibbsSampler.ConditionalSampler>();
            samplers.Add(delegate (double[] state)
            {
                return 42.0;
            });
            GibbsSampler sampler = new GibbsSampler(initialState, samplers);
            double[] result = sampler.Next();
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(42.0, result[0]);
        }

        [TestMethod]
        public void TestNullSamplers()
        {
            double[] initialState = new double[]
            {
                5.0
            };
            List<GibbsSampler.ConditionalSampler>? samplers = null;
            GibbsSampler sampler = new GibbsSampler(initialState, samplers);
            double[] result = sampler.Next();
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void TestBothNull()
        {
            double[]? initialState = null;
            List<GibbsSampler.ConditionalSampler>? samplers = null;
            GibbsSampler sampler = new GibbsSampler(initialState, samplers);
            double[] result = sampler.Next();
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void TestLargeIteration()
        {
            int length = 10;
            int iterations = 100;
            double[] initialState = new double[length];
            for (int i = 0; i < length; i++)
            {
                initialState[i] = 0.0;
            }

            List<GibbsSampler.ConditionalSampler> samplers = new List<GibbsSampler.ConditionalSampler>();
            for (int i = 0; i < length; i++)
            {
                int index = i;
                GibbsSampler.ConditionalSampler samplerDelegate = delegate (double[] state)
                {
                    return state[index] + 1.0;
                };
                samplers.Add(samplerDelegate);
            }

            GibbsSampler sampler = new GibbsSampler(initialState, samplers);
            List<double[]> samples = sampler.Run(iterations);
            Assert.AreEqual(iterations, samples.Count);
            double[] finalSample = samples[samples.Count - 1];
            Assert.AreEqual(length, finalSample.Length);
            for (int i = 0; i < length; i++)
            {
                Assert.AreEqual(100.0, finalSample[i]);
            }
        }
    }
}