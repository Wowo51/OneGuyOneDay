namespace SelfOrganizingMap
{
    public class Neuron
    {
        public double[] Weights { get; set; }

        public Neuron(double[] weights)
        {
            Weights = weights;
        }
    }
}