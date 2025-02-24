# Hmac Message Authentication

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The HmacMessageAuthentication algorithm utilizes HMAC with SHA256 to ensure the integrity and authenticity of a message using a cryptographic key. It generates a signature for a given message by hashing it along with the provided key. This mechanism guarantees that any modification to the message or use of an incorrect key will result in a different hash, thereby exposing tampering.

How to use the SourceCode:
1. Include the HmacMessageAuthentication source files in your C# project.
2. Import the HmacMessageAuthentication namespace.
3. Use the static methods ComputeHmac and ComputeHmacHex to generate a keyed-hash for your messages. ComputeHmac returns the hash as a byte array, while ComputeHmacHex returns it as a hexadecimal string.
4. Refer to the unit tests provided with the library to verify functionality and guide integration.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.