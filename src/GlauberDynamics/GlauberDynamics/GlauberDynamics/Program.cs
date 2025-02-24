namespace GlauberDynamics
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int size = 20;
            double temperature = 2.269;
            double coupling = 1.0;
            double externalField = 0.0;
            IsingModel model = new IsingModel(size, size, temperature, coupling, externalField);
            model.RandomizeSpins();
            GlauberDynamicsSimulator simulator = new GlauberDynamicsSimulator(model);
            int steps = 100000;
            simulator.Run(steps);
            System.Console.WriteLine("Simulation completed. Final energy: " + model.TotalEnergy());
        }
    }
}