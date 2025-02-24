using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructuredSvmPredictor;
using System.Reflection;

namespace StructuredSvmPredictorTest
{
    [TestClass]
    public class StructuredSvmPredictorUnitTests
    {
        [TestMethod]
        public void Svm_Predict_NullInput_ReturnsZero()
        {
            Svm svm = new Svm();
            double result = svm.Predict(null);
            Assert.AreEqual(0.0, result);
        }

        [TestMethod]
        public void StructuredSvm_PredictStructured_NullInput_ReturnsInvalidInput()
        {
            StructuredSvm structuredSvm = new StructuredSvm();
            string result = structuredSvm.PredictStructured(null);
            Assert.AreEqual("Invalid input", result);
        }

        [TestMethod]
        public void StructuredSvm_PredictStructured_WithNonEmptyInput_ReturnsPositiveLabel()
        {
            StructuredSvm structuredSvm = new StructuredSvm();
            double[] features = new double[3]
            {
                1.0,
                -2.0,
                3.0
            };
            string result = structuredSvm.PredictStructured(features);
            Assert.AreEqual("Positive Label", result);
        }

        [TestMethod]
        public void Svm_Predict_LargeData_Test()
        {
            Svm svm = new Svm();
            int largeSize = 10000;
            double[] features = new double[largeSize];
            for (int i = 0; i < largeSize; i++)
            {
                features[i] = i * 0.001;
            }

            double result = svm.Predict(features);
            Assert.AreEqual(0.0, result);
        }

        [TestMethod]
        public void StructuredSvm_PredictStructured_WithCustomWeights_ReturnsNegativeLabel()
        {
            StructuredSvm structuredSvm = new StructuredSvm();
            double[] features = new double[3]
            {
                1.0,
                2.0,
                3.0
            };
            System.Reflection.FieldInfo fieldInfo = typeof(Svm).GetField("Weights", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(fieldInfo);
            double[] customWeights = new double[3]
            {
                -1.0,
                -1.0,
                -1.0
            };
            fieldInfo.SetValue(structuredSvm, customWeights);
            string result = structuredSvm.PredictStructured(features);
            Assert.AreEqual("Negative Label", result);
        }

        [TestMethod]
        public void Svm_Predict_RandomData_Test()
        {
            Svm svm = new Svm();
            int testCount = 10;
            Random random = new Random();
            for (int t = 0; t < testCount; t++)
            {
                int size = random.Next(1, 1000);
                double[] features = new double[size];
                for (int i = 0; i < size; i++)
                {
                    features[i] = random.NextDouble() * 100 - 50;
                }

                double result = svm.Predict(features);
                Assert.AreEqual(0.0, result);
            }
        }

        [TestMethod]
        public void Svm_Predict_EmptyArray_ReturnsZero()
        {
            Svm svm = new Svm();
            double[] features = new double[0];
            double result = svm.Predict(features);
            Assert.AreEqual(0.0, result);
        }

        [TestMethod]
        public void StructuredSvm_PredictStructured_EmptyArray_ReturnsPositiveLabel()
        {
            StructuredSvm structuredSvm = new StructuredSvm();
            double[] features = new double[0];
            string result = structuredSvm.PredictStructured(features);
            Assert.AreEqual("Positive Label", result);
        }
    }
}