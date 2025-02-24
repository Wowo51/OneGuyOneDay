namespace TruncatedExponentialBackoff
{
    using System;
    using System.Threading.Tasks;

    public class TruncatedBinaryExponentialBackoff
    {
        private readonly TimeSpan _initialDelay;
        private readonly TimeSpan _maxDelay;
        private readonly Random _random;
        public TruncatedBinaryExponentialBackoff(TimeSpan initialDelay, TimeSpan maxDelay)
        {
            if (initialDelay.TotalMilliseconds < 0)
            {
                initialDelay = TimeSpan.Zero;
            }

            if (maxDelay.TotalMilliseconds < initialDelay.TotalMilliseconds)
            {
                maxDelay = initialDelay;
            }

            _initialDelay = initialDelay;
            _maxDelay = maxDelay;
            _random = new Random();
        }

        public TimeSpan GetDelay(int attempt)
        {
            if (attempt < 0)
            {
                attempt = 0;
            }

            double exponentialDelay = _initialDelay.TotalMilliseconds * Math.Pow(2, attempt);
            double boundedDelay = Math.Min(exponentialDelay, _maxDelay.TotalMilliseconds);
            int delayMilliseconds = _random.Next(0, (int)boundedDelay + 1);
            return TimeSpan.FromMilliseconds(delayMilliseconds);
        }

        public async Task DelayAsync(int attempt)
        {
            TimeSpan delay = GetDelay(attempt);
            await Task.Delay(delay);
        }
    }
}