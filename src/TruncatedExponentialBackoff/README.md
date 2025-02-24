# Truncated Exponential Backoff Overview

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The TruncatedExponentialBackoff algorithm manages retry attempts by progressively increasing the delay between each attempt in an exponential fashion while capping the delay at a maximum value. The algorithm computes a delay that doubles with each retry but is limited to a specified maximum, and it adds a randomized element to help prevent synchronized retries that could overwhelm a resource.

Usage Instructions:
1. Instantiate the TruncatedBinaryExponentialBackoff class with an initial delay and a maximum delay.
2. Use the GetDelay(attempt) method to obtain a randomized delay for the given retry attempt.
3. For asynchronous operations, call DelayAsync(attempt) to await the computed delay.
4. Integrate this backoff mechanism into your retry logic to handle transient failures in network calls or distributed systems.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.