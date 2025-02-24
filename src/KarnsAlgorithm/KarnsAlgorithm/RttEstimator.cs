using System;

namespace KarnsAlgorithm
{
    public class RttEstimator
    {
        private bool _hasInitialRtt;
        private double _estimatedRtt;
        private double _deviation;
        private const double Alpha = 0.125;
        private const double Beta = 0.25;
        public RttEstimator()
        {
            _hasInitialRtt = false;
            _estimatedRtt = 0.0;
            _deviation = 0.0;
        }

        public double EstimatedRTT
        {
            get
            {
                return _estimatedRtt;
            }
        }

        public double Deviation
        {
            get
            {
                return _deviation;
            }
        }

        public double RetransmissionTimeout
        {
            get
            {
                return _estimatedRtt + 4.0 * _deviation;
            }
        }

        public void AddRttSample(TimeSpan sample, bool isRetransmission)
        {
            if (isRetransmission)
            {
                // Karn's algorithm: Ignore ambiguous measurements from retransmissions.
                return;
            }

            double sampleMs = sample.TotalMilliseconds;
            if (!_hasInitialRtt)
            {
                _estimatedRtt = sampleMs;
                _deviation = sampleMs / 2.0;
                _hasInitialRtt = true;
            }
            else
            {
                double diff = sampleMs - _estimatedRtt;
                _estimatedRtt = (1.0 - Alpha) * _estimatedRtt + Alpha * sampleMs;
                _deviation = (1.0 - Beta) * _deviation + Beta * Abs(diff);
            }
        }

        private double Abs(double value)
        {
            if (value < 0)
            {
                return -value;
            }

            return value;
        }
    }
}