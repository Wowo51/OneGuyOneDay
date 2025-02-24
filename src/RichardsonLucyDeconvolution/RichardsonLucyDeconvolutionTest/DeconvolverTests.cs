using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardsonLucyDeconvolution;

namespace RichardsonLucyDeconvolutionTest
{
    [TestClass]
    public class DeconvolverTests
    {
        [TestMethod]
        public void TestConvolveIdentity()
        {
            double[, ] image = new double[, ]
            {
                {
                    1,
                    2,
                    3
                },
                {
                    4,
                    5,
                    6
                },
                {
                    7,
                    8,
                    9
                }
            };
            double[, ] kernel = new double[, ]
            {
                {
                    0,
                    0,
                    0
                },
                {
                    0,
                    1,
                    0
                },
                {
                    0,
                    0,
                    0
                }
            };
            double[, ] result = Deconvolver.Convolve(image, kernel);
            int height = image.GetLength(0);
            int width = image.GetLength(1);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Assert.AreEqual(image[i, j], result[i, j], 1e-9);
                }
            }
        }

        [TestMethod]
        public void TestElementWiseDivide()
        {
            double[, ] numerator = new double[, ]
            {
                {
                    4,
                    10
                },
                {
                    6,
                    9
                }
            };
            double[, ] denominator = new double[, ]
            {
                {
                    2,
                    0
                },
                {
                    3,
                    3
                }
            };
            double[, ] result = Deconvolver.ElementWiseDivide(numerator, denominator);
            double[, ] expected = new double[, ]
            {
                {
                    2,
                    0
                },
                {
                    2,
                    3
                }
            };
            int height = expected.GetLength(0);
            int width = expected.GetLength(1);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Assert.AreEqual(expected[i, j], result[i, j], 1e-9);
                }
            }
        }

        [TestMethod]
        public void TestElementWiseMultiply()
        {
            double[, ] a = new double[, ]
            {
                {
                    2,
                    3
                },
                {
                    4,
                    5
                }
            };
            double[, ] b = new double[, ]
            {
                {
                    5,
                    6
                },
                {
                    7,
                    8
                }
            };
            double[, ] result = Deconvolver.ElementWiseMultiply(a, b);
            double[, ] expected = new double[, ]
            {
                {
                    10,
                    18
                },
                {
                    28,
                    40
                }
            };
            int height = expected.GetLength(0);
            int width = expected.GetLength(1);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Assert.AreEqual(expected[i, j], result[i, j], 1e-9);
                }
            }
        }

        [TestMethod]
        public void TestReflect()
        {
            double[, ] kernel = new double[, ]
            {
                {
                    1,
                    2
                },
                {
                    3,
                    4
                }
            };
            double[, ] result = Deconvolver.Reflect(kernel);
            double[, ] expected = new double[, ]
            {
                {
                    4,
                    3
                },
                {
                    2,
                    1
                }
            };
            int height = expected.GetLength(0);
            int width = expected.GetLength(1);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Assert.AreEqual(expected[i, j], result[i, j], 1e-9);
                }
            }
        }

        [TestMethod]
        public void TestCopyArray()
        {
            double[, ] original = new double[, ]
            {
                {
                    1,
                    2
                },
                {
                    3,
                    4
                }
            };
            double[, ] copy = Deconvolver.CopyArray(original);
            int height = original.GetLength(0);
            int width = original.GetLength(1);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Assert.AreEqual(original[i, j], copy[i, j], 1e-9);
                }
            }

            Assert.IsFalse(object.ReferenceEquals(original, copy));
        }

        [TestMethod]
        public void TestDeconvolveIdentity()
        {
            double[, ] blurred = new double[, ]
            {
                {
                    1,
                    2,
                    3
                },
                {
                    4,
                    5,
                    6
                },
                {
                    7,
                    8,
                    9
                }
            };
            double[, ] identityPsf = new double[, ]
            {
                {
                    0,
                    0,
                    0
                },
                {
                    0,
                    1,
                    0
                },
                {
                    0,
                    0,
                    0
                }
            };
            int iterations = 5;
            double[, ] result = Deconvolver.Deconvolve(blurred, identityPsf, iterations);
            int height = blurred.GetLength(0);
            int width = blurred.GetLength(1);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Assert.AreEqual(blurred[i, j], result[i, j], 1e-9);
                }
            }
        }

        [TestMethod]
        public void TestSyntheticLargeArray()
        {
            int height = 50;
            int width = 50;
            double[, ] image = new double[height, width];
            System.Random random = new System.Random(42);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    image[i, j] = random.NextDouble() * 100;
                }
            }

            double[, ] identityPsf = new double[, ]
            {
                {
                    0,
                    0,
                    0
                },
                {
                    0,
                    1,
                    0
                },
                {
                    0,
                    0,
                    0
                }
            };
            int iterations = 3;
            double[, ] result = Deconvolver.Deconvolve(image, identityPsf, iterations);
            Assert.AreEqual(image.GetLength(0), result.GetLength(0));
            Assert.AreEqual(image.GetLength(1), result.GetLength(1));
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Assert.AreEqual(image[i, j], result[i, j], 1e-9);
                }
            }
        }
    }
}