namespace EvolutionStrategy
{
    public class Individual
    {
        public double[] Parameters;
        public double Fitness;
        public Individual(double[] parameters)
        {
            Parameters = parameters;
        }

        public void Evaluate()
        {
            double sumSquares = 0.0;
            for (int i = 0; i < Parameters.Length; i++)
            {
                sumSquares += Parameters[i] * Parameters[i];
            }

            Fitness = -sumSquares;
        }
    }
}