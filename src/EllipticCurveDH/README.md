# EllipticCurveDH Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT license.

The EllipticCurveDH algorithm implements an elliptic-curve Diffie–Hellman key exchange mechanism which allows two parties to securely generate a shared secret over an insecure channel. The algorithm leverages .NET’s ECDiffieHellman class to create a public-private key pair and derive a shared secret using another party’s public key.

To use the SourceCode, simply include the provided C# library in your project. Reference the library, instantiate ECDHKeyExchange, retrieve the PublicKey, and call DeriveSharedSecret() with a peer’s public key to compute the shared secret. Comprehensive unit tests are provided to demonstrate proper usage and to validate key agreement as well as error handling with null or invalid inputs.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>