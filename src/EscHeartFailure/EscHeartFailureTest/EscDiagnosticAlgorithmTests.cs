using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using EscHeartFailure;

namespace EscHeartFailureTest
{
    [TestClass]
    public class EscDiagnosticAlgorithmTests
    {
        [TestMethod]
        public void Diagnose_NullPatient_ReturnsInvalidPatientMessage()
        {
            EscDiagnosticAlgorithm diagnostic = new EscDiagnosticAlgorithm();
            string result = diagnostic.Diagnose(null);
            Assert.AreEqual("Invalid patient data.", result);
        }

        [TestMethod]
        public void Diagnose_HasSymptomsAndHighBNP_ReturnsLikely()
        {
            EscDiagnosticAlgorithm diagnostic = new EscDiagnosticAlgorithm();
            PatientData patient = new PatientData();
            patient.HasSymptoms = true;
            patient.BNPLevel = 101.0;
            string result = diagnostic.Diagnose(patient);
            Assert.AreEqual("Heart failure likely", result);
        }

        [TestMethod]
        public void Diagnose_HasSymptomsAndBorderlineBNP_ReturnsFurtherTestsNeeded()
        {
            EscDiagnosticAlgorithm diagnostic = new EscDiagnosticAlgorithm();
            PatientData patient = new PatientData();
            patient.HasSymptoms = true;
            patient.BNPLevel = 100.0;
            string result = diagnostic.Diagnose(patient);
            Assert.AreEqual("Further tests needed", result);
        }

        [TestMethod]
        public void Diagnose_NoSymptoms_ReturnsUnlikely()
        {
            EscDiagnosticAlgorithm diagnostic = new EscDiagnosticAlgorithm();
            PatientData patient = new PatientData();
            patient.HasSymptoms = false;
            patient.BNPLevel = 120.0;
            string result = diagnostic.Diagnose(patient);
            Assert.AreEqual("Heart failure unlikely", result);
        }

        [TestMethod]
        public void Diagnose_RandomBNP_Test()
        {
            EscDiagnosticAlgorithm diagnostic = new EscDiagnosticAlgorithm();
            for (int index = 0; index < 1000; index = index + 1)
            {
                double bnp = 90.0 + (index % 20);
                PatientData patient = new PatientData();
                patient.HasSymptoms = true;
                patient.BNPLevel = bnp;
                string result = diagnostic.Diagnose(patient);
                if (bnp > 100.0)
                {
                    Assert.AreEqual("Heart failure likely", result);
                }
                else
                {
                    Assert.AreEqual("Further tests needed", result);
                }
            }
        }

        [TestMethod]
        public void Diagnose_RandomBNP_WithRandomGen_Test()
        {
            EscDiagnosticAlgorithm diagnostic = new EscDiagnosticAlgorithm();
            Random random = new Random(42);
            for (int i = 0; i < 500; i = i + 1)
            {
                double bnp = random.NextDouble() * 100.0 + 50.0;
                PatientData patient = new PatientData();
                patient.HasSymptoms = true;
                patient.BNPLevel = bnp;
                string result = diagnostic.Diagnose(patient);
                if (bnp > 100.0)
                {
                    Assert.AreEqual("Heart failure likely", result);
                }
                else
                {
                    Assert.AreEqual("Further tests needed", result);
                }
            }
        }
    }
}