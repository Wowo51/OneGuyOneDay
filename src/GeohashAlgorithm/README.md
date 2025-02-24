# Geohash Algorithm Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The GeohashAlgorithm algorithm encodes geographic coordinates (latitude and longitude) into a short alphanumeric string. It works by iteratively subdividing the coordinate space and encoding the resulting sequence of spatial divisions using a base32 encoding scheme. To use the SourceCode, integrate the library into your .NET project and invoke the Geohash.Encode method with the desired latitude, longitude, and precision. Unit tests are provided to verify the implementation.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.
Authored by Warren Harding. AI assisted.