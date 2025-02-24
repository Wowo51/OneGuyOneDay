# DiffieHellmanExchange Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

DiffieHellmanExchange implements the Diffie-Hellman key exchange protocol, which allows two parties to securely establish a shared secret over an insecure channel. Each participant selects a private key, computes a corresponding public key using modular exponentiation, and then exchanges public keys with the other party. By combining their private key with the other party's public key, both arrive at the same shared secret.

To use the SourceCode, open the solution file (DiffieHellmanExchange.sln) in Visual Studio or use the dotnet CLI to compile and run the project. The repository includes comprehensive unit tests using MSTest to validate the implementation as well as a sample Program demonstrating a successful key exchange.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>