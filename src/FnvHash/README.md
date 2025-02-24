# FnvHash Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The FnvHash algorithm is a fast, non-cryptographic hash function that computes a 32-bit hash value from input data. It starts with a predefined offset basis and iteratively XORs each byte with the hash value before multiplying by a prime number. This method provides efficient computation and a low collision rate, making it well-suited for hash table and lookup applications.

How to use the SourceCode:
1. Include the FnvHash library in your project.
2. Call FnvHasher.ComputeHash(string) or FnvHasher.ComputeHash(byte[]) to generate a hash value.
3. Ensure that input strings are encoded using UTF8 to maintain consistency.
4. Run the provided unit tests to verify that the implementation works as expected.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.