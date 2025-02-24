using System;

namespace ApproximateCountingAlgorithm
{
    // Implementation of the Morris approximate counting algorithm.
    public class MorrisCounter
    {
        private int _counter;
        private readonly Random _random;
        public MorrisCounter()
        {
            _counter = 0;
            _random = new Random();
        }

        // Increments the counter probabilistically.
        public void Increment()
        {
            double probability = Math.Pow(2.0, -_counter);
            double randomValue = _random.NextDouble();
            if (randomValue < probability)
            {
                _counter++;
            }
        }

        // Returns the approximate count based on the current counter value.
        public double GetCount()
        {
            return Math.Pow(2.0, _counter) - 1;
        }

        // Resets the counter to its initial state.
        public void Reset()
        {
            _counter = 0;
        }
    }
}