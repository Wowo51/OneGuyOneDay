using System;
using System.Threading.Tasks;

namespace ExponentialBackoff
{
    public static class ExponentialBackoffHelper
    {
        public static async Task ExecuteAsync(Func<Task<bool>> operation, int maxRetries = 5, int initialDelayMs = 100, double multiplier = 2.0, int maxDelayMs = 10000)
        {
            if (operation == null)
            {
                return;
            }

            int attempt = 0;
            int delayMs = initialDelayMs;
            while (true)
            {
                bool result = await operation();
                if (result)
                {
                    break;
                }

                attempt++;
                if (attempt >= maxRetries)
                {
                    break;
                }

                await Task.Delay(delayMs);
                int newDelay = (int)(delayMs * multiplier);
                delayMs = newDelay > maxDelayMs ? maxDelayMs : newDelay;
            }
        }
    }
}