using System;

namespace EscHeartFailure
{
    public class Program
    {
        public static void Main(string[] args)
        {
            EscDiagnosticAlgorithm diagnostic = new EscDiagnosticAlgorithm();
            PatientData patient = new PatientData
            {
                HasSymptoms = true,
                BNPLevel = 150.0
            };
            string diagnosis = diagnostic.Diagnose(patient);
            Console.WriteLine("Diagnosis: " + diagnosis);
        }
    }
}