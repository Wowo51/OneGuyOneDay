using System;

namespace KarplusStrongSynthesis
{
    public class KarplusStrongSynth
    {
        private readonly double[] _buffer;
        private readonly int _bufferLength;
        private int _currentIndex;
        public int SampleRate { get; }
        public double Frequency { get; }
        public double Decay { get; }

        public KarplusStrongSynth(int sampleRate, double frequency, double decay)
        {
            if (sampleRate <= 0)
            {
                sampleRate = 44100;
            }

            if (frequency <= 0)
            {
                frequency = 440.0;
            }

            SampleRate = sampleRate;
            Frequency = frequency;
            Decay = decay;
            _bufferLength = SampleRate / (int)frequency;
            if (_bufferLength < 2)
            {
                _bufferLength = 2;
            }

            _buffer = new double[_bufferLength];
            InitializeBuffer();
            _currentIndex = 0;
        }

        private void InitializeBuffer()
        {
            Random random = new Random();
            for (int index = 0; index < _bufferLength; index++)
            {
                _buffer[index] = random.NextDouble() * 2.0 - 1.0;
            }
        }

        public double[] GenerateSamples(int numSamples)
        {
            if (numSamples <= 0)
            {
                return new double[0];
            }

            double[] outputSamples = new double[numSamples];
            for (int i = 0; i < numSamples; i++)
            {
                double currentSample = _buffer[_currentIndex];
                int nextIndex = (_currentIndex + 1) % _bufferLength;
                double newSample = Decay * 0.5 * (currentSample + _buffer[nextIndex]);
                _buffer[_currentIndex] = newSample;
                outputSamples[i] = currentSample;
                _currentIndex = nextIndex;
            }

            return outputSamples;
        }
    }
}