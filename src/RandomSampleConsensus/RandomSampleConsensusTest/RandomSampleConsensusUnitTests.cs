using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomSampleConsensus;

namespace RandomSampleConsensusTest
{
    [TestClass]
    public class RansacEstimatorTests
    {
        [TestMethod]
        public void Estimate_ReturnsDefault_WhenDataIsNull()
        {
            RansacEstimator<double, double> estimator = new RansacEstimator<double, double>(10, 0.001, 3, 3);
            List<double>? data = null;
            double result = estimator.Estimate(data, delegate (List<double> sample)
            {
                double sum = 0.0;
                foreach (double d in sample)
                {
                    sum += d;
                }

                return sum / sample.Count;
            }, delegate (double model, double datum)
            {
                return Math.Abs(model - datum);
            });
            Assert.AreEqual(default(double), result);
        }

        [TestMethod]
        public void Estimate_ReturnsDefault_WhenDataIsInsufficient()
        {
            RansacEstimator<double, double> estimator = new RansacEstimator<double, double>(10, 0.001, 3, 3);
            List<double> data = new List<double>
            {
                1.0,
                2.0
            };
            double result = estimator.Estimate(data, delegate (List<double> sample)
            {
                double sum = 0.0;
                foreach (double d in sample)
                {
                    sum += d;
                }

                return sum / sample.Count;
            }, delegate (double model, double datum)
            {
                return Math.Abs(model - datum);
            });
            Assert.AreEqual(default(double), result);
        }

        [TestMethod]
        public void Estimate_ReturnsCorrectModel_ForConstantData()
        {
            RansacEstimator<double, double> estimator = new RansacEstimator<double, double>(20, 0.001, 3, 3);
            List<double> data = new List<double>();
            for (int i = 0; i < 10; i++)
            {
                data.Add(5.0);
            }

            double result = estimator.Estimate(data, delegate (List<double> sample)
            {
                double sum = 0.0;
                foreach (double d in sample)
                {
                    sum += d;
                }

                return sum / sample.Count;
            }, delegate (double model, double datum)
            {
                return Math.Abs(model - datum);
            });
            Assert.AreEqual(5.0, result, 0.0001);
        }

        [TestMethod]
        public void Estimate_ReturnsModel_ForNoisyData()
        {
            RansacEstimator<double, double> estimator = new RansacEstimator<double, double>(50, 1.0, 3, 15);
            List<double> data = new List<double>();
            Random rand = new Random(42);
            for (int i = 0; i < 20; i++)
            {
                double noise = (rand.NextDouble() - 0.5) * 0.5;
                data.Add(10.0 + noise);
            }

            for (int i = 0; i < 5; i++)
            {
                double noise = (rand.NextDouble() - 0.5) * 0.5;
                data.Add(50.0 + noise);
            }

            double model = estimator.Estimate(data, delegate (List<double> sample)
            {
                double sum = 0.0;
                foreach (double d in sample)
                {
                    sum += d;
                }

                return sum / sample.Count;
            }, delegate (double modelValue, double datum)
            {
                return Math.Abs(modelValue - datum);
            });
            Assert.IsTrue(Math.Abs(model - 10.0) < 1.0);
        }

        [TestMethod]
        public void Estimate_ReturnsDefault_WhenNoConsensusFound()
        {
            RansacEstimator<double, double> estimator = new RansacEstimator<double, double>(20, 0.0001, 3, 8);
            List<double> data = new List<double>
            {
                1.0,
                2.0,
                3.0,
                4.0,
                5.0,
                6.0,
                7.0,
                8.0
            };
            double result = estimator.Estimate(data, delegate (List<double> sample)
            {
                double sum = 0.0;
                foreach (double d in sample)
                {
                    sum += d;
                }

                return sum / sample.Count;
            }, delegate (double model, double datum)
            {
                return Math.Abs(model - datum);
            });
            Assert.AreEqual(default(double), result);
        }

        [TestMethod]
        public void Estimate_ReturnsCorrectModel_ForLargeConstantDataset()
        {
            int size = 1000;
            RansacEstimator<double, double> estimator = new RansacEstimator<double, double>(100, 0.05, 3, 900);
            List<double> data = new List<double>();
            for (int i = 0; i < size; i++)
            {
                data.Add(7.5);
            }

            double result = estimator.Estimate(data, delegate (List<double> sample)
            {
                double sum = 0.0;
                foreach (double d in sample)
                {
                    sum += d;
                }

                return sum / sample.Count;
            }, delegate (double model, double datum)
            {
                return Math.Abs(model - datum);
            });
            Assert.AreEqual(7.5, result, 0.0001);
        }
    }
}