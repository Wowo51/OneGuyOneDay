
# Adaptive Replacement Cache

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Adaptive Replacement Cache (ARC) algorithm is a sophisticated caching mechanism that dynamically balances between recency (most recently used items) and frequency (most frequently used items). ARC maintains two primary lists: one for items that have been used recently but not repeatedly, and another for items that are accessed frequently. It also employs ghost lists to track items that have been evicted, allowing the algorithm to adapt to changing access patterns. This dynamic adjustment of cache behavior helps optimize performance under varying workloads.

To use the source code, simply include the provided AdaptiveReplacementCache library into your C# solution. The implementation consists of the main cache logic as well as unit tests using MSTest. Reference the main project to leverage the caching functionality and run the tests to verify operations such as insertion (Put), retrieval (Get), and eviction handling. The code is structured for easy integration and further enhancement if needed.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
  