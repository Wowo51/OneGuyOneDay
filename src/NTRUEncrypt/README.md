# NTRUEncrypt Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

NTRUEncrypt is a lattice-based public-key encryption algorithm that operates over polynomial rings. The algorithm generates key pairs by constructing polynomials with coefficients chosen from a limited set (typically -1, 0, and 1). Encryption is performed by converting the message into a polynomial and combining it with the public key polynomial using modular arithmetic. Decryption reverses the process by subtracting the private key polynomial, thereby recovering the original message.

How to Use the Source Code:
- The library is implemented entirely in C# and relies only on Microsoft's unit testing framework.
- The source code is divided into multiple files, each addressing specific functionalities such as key pair generation, polynomial operations, encryption, and decryption.
- To use the library, open the provided solution in Visual Studio and run the unit tests to verify its functionality.
- You can integrate the code into your projects as a reference implementation of the NTRUEncrypt algorithm.
- The implementation is distributed under the MIT license, which allows free use, modification, and distribution.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>