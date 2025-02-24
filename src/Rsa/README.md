# RSA Algorithm Implementation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

RSA Algorithm Help:
The RSA algorithm is a public-key cryptography method that uses two large prime numbers to generate a pair of keys—a public key for encryption and a private key for decryption. The algorithm works by computing the modulus from the primes and Euler’s totient to determine the keys. A valid public exponent is chosen that is coprime with the totient, and the private key is calculated as the modular inverse of the public exponent.

How to Use the SourceCode:
- Instantiate the RsaAlgorithm class with two prime numbers (p and q) and a valid public exponent (e).
- Use the Encrypt method to convert plaintext messages into ciphertext.
- Use the Decrypt method to revert the ciphertext back to the original message.
- Run the provided unit tests to validate the encryption-decryption process.
- Integrate the library into your project following the standard C# project structure.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>