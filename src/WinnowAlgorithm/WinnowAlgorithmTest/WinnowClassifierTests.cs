using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinnowAlgorithm;

namespace WinnowAlgorithmTest
{
    [TestClass]
    public class WinnowClassifierTests
    {
        [TestMethod]
        public void Predict_NullFeatures_ReturnsFalse()
        {
            WinnowClassifier classifier = new WinnowClassifier(3, 2, 2);
            bool prediction = classifier.Predict(null);
            Assert.IsFalse(prediction);
        }

        [TestMethod]
        public void Predict_SimpleCases()
        {
            WinnowClassifier classifier = new WinnowClassifier(3, 2, 2);
            bool[] features1 = new bool[]
            {
                true,
                false,
                false
            };
            bool[] features2 = new bool[]
            {
                true,
                true,
                false
            };
            bool result1 = classifier.Predict(features1);
            bool result2 = classifier.Predict(features2);
            Assert.IsFalse(result1);
            Assert.IsTrue(result2);
        }

        [TestMethod]
        public void Update_IncreasesWeightsForFalsePredictionWithTrueLabel()
        {
            double threshold = 3;
            double alpha = 2;
            WinnowClassifier classifier = new WinnowClassifier(3, threshold, alpha);
            bool[] features = new bool[]
            {
                true,
                true,
                false
            };
            bool initialPrediction = classifier.Predict(features);
            Assert.IsFalse(initialPrediction);
            classifier.Update(features, true);
            double[] weights = classifier.GetWeights();
            Assert.AreEqual(2.0, weights[0]);
            Assert.AreEqual(2.0, weights[1]);
            Assert.AreEqual(1.0, weights[2]);
            bool newPrediction = classifier.Predict(features);
            Assert.IsTrue(newPrediction);
        }

        [TestMethod]
        public void Update_DecreasesWeightsForTruePredictionWithFalseLabel()
        {
            double threshold = 2;
            double alpha = 2;
            WinnowClassifier classifier = new WinnowClassifier(3, threshold, alpha);
            bool[] features = new bool[]
            {
                true,
                true,
                false
            };
            bool initialPrediction = classifier.Predict(features);
            Assert.IsTrue(initialPrediction);
            classifier.Update(features, false);
            double[] weights = classifier.GetWeights();
            Assert.AreEqual(0.5, weights[0]);
            Assert.AreEqual(0.5, weights[1]);
            Assert.AreEqual(1.0, weights[2]);
            bool newPrediction = classifier.Predict(features);
            Assert.IsFalse(newPrediction);
        }

        [TestMethod]
        public void Predict_LargeDataSet()
        {
            int featureCount = 1000;
            double threshold = 500;
            double alpha = 2;
            WinnowClassifier classifier = new WinnowClassifier(featureCount, threshold, alpha);
            bool[] largeFeatures = new bool[featureCount];
            for (int i = 0; i < 500; i++)
            {
                largeFeatures[i] = true;
            }

            bool prediction = classifier.Predict(largeFeatures);
            Assert.IsTrue(prediction);
        }

        [TestMethod]
        public void Update_NoChange_WhenPredictionIsCorrect()
        {
            int featureCount = 5;
            double threshold = 2;
            double alpha = 2;
            WinnowClassifier classifier = new WinnowClassifier(featureCount, threshold, alpha);
            bool[] features = new bool[]
            {
                true,
                false,
                false,
                false,
                false
            };
            bool predictionBefore = classifier.Predict(features);
            Assert.IsFalse(predictionBefore);
            classifier.Update(features, false);
            double[] weights = classifier.GetWeights();
            double[] expectedWeights = new double[]
            {
                1.0,
                1.0,
                1.0,
                1.0,
                1.0
            };
            CollectionAssert.AreEqual(expectedWeights, weights);
        }

        [TestMethod]
        public void Update_NullFeatures_DoesNothing()
        {
            WinnowClassifier classifier = new WinnowClassifier(3, 2, 2);
            double[] beforeWeights = classifier.GetWeights();
            classifier.Update(null, true);
            double[] afterWeights = classifier.GetWeights();
            CollectionAssert.AreEqual(beforeWeights, afterWeights);
        }
    }
}