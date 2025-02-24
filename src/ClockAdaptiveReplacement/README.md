
# Clock Adaptive Replacement Cache

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

ClockAdaptiveReplacement is an adaptive cache management algorithm that enhances performance through intelligent page replacement. It maintains two primary lists: T1 for newly added and less frequently accessed items, and T2 for frequently accessed items. In addition, ghost lists track recently evicted keys to adaptively adjust cache behavior. The algorithm uses a clock hand mechanism to traverse the cache where each entry has a reference bit; if the bit is set, it is cleared, otherwise the entry is eligible for eviction based on its designated list. This approach allows dynamic tuning of cache replacement, reducing miss rates under varying workloads.

To use the SourceCode, simply add the ClockAdaptiveReplacement project to your solution. Instantiate the CARCache<TKey, TValue> class to create your cache, and then use the Put and TryGetValue methods to manage cache entries. Unit tests included in the project illustrate common usage patterns and verify the correctness of the algorithm.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
  