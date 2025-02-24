# Salsa20AndChaCha20 Help

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

The Salsa20AndChaCha20 library implements two high-performance stream ciphers: Salsa20 and ChaCha20. Salsa20 is renowned for its simplicity and speed, while ChaCha20 offers enhanced security and robustness through additional round variations. Both algorithms are designed for efficient encryption and decryption of data.

To use the source code, include the provided project in your solution and reference it in your application. Create an instance of the Salsa20 or ChaCha20 class by supplying a 32-byte key and an appropriate nonce (8 bytes for Salsa20 and 12 bytes for ChaCha20). Then, use the ProcessBytes method to encrypt or decrypt your data. The unit tests supplied with the project illustrate typical usage scenarios and error handling practices.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.