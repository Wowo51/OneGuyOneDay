namespace GlauberDynamics
{
    public class GlauberDynamicsSimulator
    {
        private IsingModel Model;
        public GlauberDynamicsSimulator(IsingModel model)
        {
            this.Model = model;
        }

        public void Step()
        {
            int row = Model.Random.Next(0, Model.Rows);
            int col = Model.Random.Next(0, Model.Columns);
            double deltaE = Model.CalculateDeltaEnergy(row, col);
            double probability = 1.0 / (1.0 + System.Math.Exp(deltaE / Model.Temperature));
            if (Model.Random.NextDouble() < probability)
            {
                Model.Spins[row, col] = -Model.Spins[row, col];
            }
        }

        public void Run(int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                Step();
            }
        }
    }
}