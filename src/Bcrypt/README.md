# Bcrypt Algorithm Overview

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Bcrypt algorithm is designed for secure password hashing. It uses a cost factor which can be adjusted to increase the computational effort required to generate the hash, making brute-force attacks less feasible. This implementation creates a random salt and then performs iterative hashing using PBKDF2 with the SHA256 algorithm, before encoding the result using a custom base64 mapping similar to the original bcrypt algorithm.

To use the source code, integrate the provided C# library into your project. Call the HashPassword method with your plain text password to generate a secure hash, and use the VerifyPassword method to check a password against the hash. The source is organized into multiple files including the main Bcrypt class (Bcrypt.cs), unit tests (BcryptTests.cs), and supporting project files for Visual Studio. It is fully unit tested and free to use under the MIT license.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.