using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViterbiAlgorithm;

namespace ViterbiAlgorithmTest
{
    [TestClass]
    public class ViterbiAlgorithmTests
    {
        [TestMethod]
        public void Test_NullModel_ReturnsEmptyList()
        {
            List<string> observations = new List<string>
            {
                "Obs1"
            };
            List<string> result = Viterbi.FindMostLikelySequence(null, observations);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Test_NullObservationSequence_ReturnsEmptyList()
        {
            List<string> states = new List<string>
            {
                "S1",
                "S2"
            };
            List<string> obs = new List<string>
            {
                "O1",
                "O2"
            };
            double[] startProb = new double[]
            {
                0.5,
                0.5
            };
            double[, ] transitionProb = new double[, ]
            {
                {
                    0.7,
                    0.3
                },
                {
                    0.4,
                    0.6
                }
            };
            double[, ] emissionProb = new double[, ]
            {
                {
                    0.6,
                    0.4
                },
                {
                    0.5,
                    0.5
                }
            };
            HiddenMarkovModel model = new HiddenMarkovModel(states, obs, startProb, transitionProb, emissionProb);
            List<string> result = Viterbi.FindMostLikelySequence(model, null);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Test_EmptyObservationSequence_ReturnsEmptyList()
        {
            List<string> states = new List<string>
            {
                "S1",
                "S2"
            };
            List<string> obs = new List<string>
            {
                "O1",
                "O2"
            };
            double[] startProb = new double[]
            {
                0.5,
                0.5
            };
            double[, ] transitionProb = new double[, ]
            {
                {
                    0.7,
                    0.3
                },
                {
                    0.4,
                    0.6
                }
            };
            double[, ] emissionProb = new double[, ]
            {
                {
                    0.6,
                    0.4
                },
                {
                    0.5,
                    0.5
                }
            };
            HiddenMarkovModel model = new HiddenMarkovModel(states, obs, startProb, transitionProb, emissionProb);
            List<string> emptySequence = new List<string>();
            List<string> result = Viterbi.FindMostLikelySequence(model, emptySequence);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Test_SingleObservation_ReturnsCorrectSequence()
        {
            List<string> states = new List<string>
            {
                "Sunny",
                "Rainy"
            };
            List<string> obs = new List<string>
            {
                "Dry",
                "Wet"
            };
            double[] startProb = new double[]
            {
                0.6,
                0.4
            };
            double[, ] transitionProb = new double[, ]
            {
                {
                    0.7,
                    0.3
                },
                {
                    0.4,
                    0.6
                }
            };
            double[, ] emissionProb = new double[, ]
            {
                {
                    0.9,
                    0.1
                },
                {
                    0.2,
                    0.8
                }
            };
            HiddenMarkovModel model = new HiddenMarkovModel(states, obs, startProb, transitionProb, emissionProb);
            List<string> sequence = new List<string>
            {
                "Dry"
            };
            List<string> result = Viterbi.FindMostLikelySequence(model, sequence);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Sunny", result[0]);
        }

        [TestMethod]
        public void Test_KnownExample_MultipleObservations()
        {
            List<string> states = new List<string>
            {
                "Rainy",
                "Sunny"
            };
            List<string> obs = new List<string>
            {
                "walk",
                "shop",
                "clean"
            };
            double[] startProb = new double[]
            {
                0.6,
                0.4
            };
            double[, ] transitionProb = new double[, ]
            {
                {
                    0.7,
                    0.3
                },
                {
                    0.4,
                    0.6
                }
            };
            double[, ] emissionProb = new double[, ]
            {
                {
                    0.1,
                    0.4,
                    0.5
                },
                {
                    0.6,
                    0.3,
                    0.1
                }
            };
            HiddenMarkovModel model = new HiddenMarkovModel(states, obs, startProb, transitionProb, emissionProb);
            List<string> obsSeq = new List<string>
            {
                "walk",
                "shop",
                "clean"
            };
            List<string> result = Viterbi.FindMostLikelySequence(model, obsSeq);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("Sunny", result[0]);
            Assert.AreEqual("Rainy", result[1]);
            Assert.AreEqual("Rainy", result[2]);
        }

        [TestMethod]
        public void Test_ObservationNotFound_UsesZeroEmissionProbability()
        {
            List<string> states = new List<string>
            {
                "S1",
                "S2"
            };
            List<string> obs = new List<string>
            {
                "A",
                "B"
            };
            double[] startProb = new double[]
            {
                0.5,
                0.5
            };
            double[, ] transitionProb = new double[, ]
            {
                {
                    0.8,
                    0.2
                },
                {
                    0.3,
                    0.7
                }
            };
            double[, ] emissionProb = new double[, ]
            {
                {
                    0.9,
                    0.1
                },
                {
                    0.2,
                    0.8
                }
            };
            HiddenMarkovModel model = new HiddenMarkovModel(states, obs, startProb, transitionProb, emissionProb);
            List<string> obsSeq = new List<string>
            {
                "C"
            };
            List<string> result = Viterbi.FindMostLikelySequence(model, obsSeq);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("S1", result[0]);
        }

        [TestMethod]
        public void Test_LargeSyntheticData()
        {
            Random random = new Random(0);
            int numStates = 10;
            int numObservations = 20;
            List<string> states = new List<string>();
            for (int i = 0; i < numStates; i++)
            {
                states.Add("State" + i.ToString());
            }

            List<string> obs = new List<string>();
            for (int j = 0; j < numObservations; j++)
            {
                obs.Add("Obs" + j.ToString());
            }

            double[] startProb = new double[numStates];
            double[, ] transitionProb = new double[numStates, numStates];
            double[, ] emissionProb = new double[numStates, numObservations];
            double totalStart = 0.0;
            for (int i = 0; i < numStates; i++)
            {
                double p = random.NextDouble();
                startProb[i] = p;
                totalStart += p;
            }

            for (int i = 0; i < numStates; i++)
            {
                startProb[i] = startProb[i] / totalStart;
            }

            for (int i = 0; i < numStates; i++)
            {
                double rowSum = 0.0;
                for (int j = 0; j < numStates; j++)
                {
                    double p = random.NextDouble();
                    transitionProb[i, j] = p;
                    rowSum += p;
                }

                for (int j = 0; j < numStates; j++)
                {
                    transitionProb[i, j] /= rowSum;
                }
            }

            for (int i = 0; i < numStates; i++)
            {
                double rowSum = 0.0;
                for (int j = 0; j < numObservations; j++)
                {
                    double p = random.NextDouble();
                    emissionProb[i, j] = p;
                    rowSum += p;
                }

                for (int j = 0; j < numObservations; j++)
                {
                    emissionProb[i, j] /= rowSum;
                }
            }

            HiddenMarkovModel model = new HiddenMarkovModel(states, obs, startProb, transitionProb, emissionProb);
            List<string> obsSeq = new List<string>();
            int sequenceLength = 1000;
            for (int t = 0; t < sequenceLength; t++)
            {
                int obsIndex = random.Next(0, numObservations);
                obsSeq.Add(obs[obsIndex]);
            }

            List<string> result = Viterbi.FindMostLikelySequence(model, obsSeq);
            Assert.IsNotNull(result);
            Assert.AreEqual(sequenceLength, result.Count);
        }

        [TestMethod]
        public void Test_EmptyStates_ReturnsEmptyList()
        {
            List<string> states = new List<string>();
            List<string> obs = new List<string>
            {
                "O1",
                "O2"
            };
            double[] startProb = new double[0];
            double[, ] transitionProb = new double[0, 0];
            double[, ] emissionProb = new double[0, 0];
            HiddenMarkovModel model = new HiddenMarkovModel(states, obs, startProb, transitionProb, emissionProb);
            List<string> observationSequence = new List<string>
            {
                "O1"
            };
            List<string> result = Viterbi.FindMostLikelySequence(model, observationSequence);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }
    }
}