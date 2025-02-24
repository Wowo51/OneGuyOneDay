using System;

namespace EscHeartFailure
{
    public class EscDiagnosticAlgorithm
    {
        private const double BnpThreshold = 100.0;
        public string Diagnose(PatientData patient)
        {
            if (patient == null)
            {
                return "Invalid patient data.";
            }

            if (patient.HasSymptoms)
            {
                if (patient.BNPLevel > BnpThreshold)
                {
                    return "Heart failure likely";
                }
                else
                {
                    return "Further tests needed";
                }
            }
            else
            {
                return "Heart failure unlikely";
            }
        }
    }
}