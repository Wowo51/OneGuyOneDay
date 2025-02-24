# RC4 Cipher Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The RC4 Cipher algorithm is a symmetric stream cipher that uses a key scheduling algorithm and a pseudo-random generation algorithm. In this implementation, the algorithm initializes a state array using a key, then generates a pseudo-random byte stream which is XORed with the input data to produce the encrypted or decrypted output. To use the library, include it in your C# project, call RC4Cipher.Encrypt to encrypt your data, and RC4Cipher.Decrypt to convert it back. The provided source code includes unit tests demonstrating various scenarios, from null and empty input handling to full encryption and decryption of data. This documentation serves as a quick reference guide to the RC4 Cipher implementation and demonstrates its integration within your projects.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.