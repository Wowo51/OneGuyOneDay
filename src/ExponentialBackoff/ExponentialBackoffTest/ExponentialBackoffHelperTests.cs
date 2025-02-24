using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExponentialBackoff;

namespace ExponentialBackoffTest
{
    [TestClass]
    public class ExponentialBackoffHelperTests
    {
        [TestMethod]
        public async Task TestOperationSucceedsImmediately()
        {
            int attempts = 0;
            Func<Task<bool>> operation = () =>
            {
                attempts++;
                return Task.FromResult(true);
            };
            await ExponentialBackoffHelper.ExecuteAsync(operation, 3, 1, 1.0, 1);
            Assert.AreEqual(1, attempts);
        }

        [TestMethod]
        public async Task TestOperationSucceedsAfterRetries()
        {
            int attempts = 0;
            int succeedAt = 3;
            Func<Task<bool>> operation = () =>
            {
                attempts++;
                return Task.FromResult(attempts >= succeedAt);
            };
            await ExponentialBackoffHelper.ExecuteAsync(operation, 5, 1, 1.0, 1);
            Assert.AreEqual(succeedAt, attempts);
        }

        [TestMethod]
        public async Task TestOperationAlwaysFails()
        {
            int attempts = 0;
            int maxRetries = 4;
            Func<Task<bool>> operation = () =>
            {
                attempts++;
                return Task.FromResult(false);
            };
            await ExponentialBackoffHelper.ExecuteAsync(operation, maxRetries, 1, 1.0, 1);
            Assert.AreEqual(maxRetries, attempts);
        }

        [TestMethod]
        public async Task TestNullOperation()
        {
            await ExponentialBackoffHelper.ExecuteAsync(null, 3, 1, 1.0, 1);
            Assert.IsTrue(true);
        }
    }
}