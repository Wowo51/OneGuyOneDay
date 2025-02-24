
# Longitudinal Redundancy Check Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

## Help: Longitudinal Redundancy Check (LRC)

The Longitudinal Redundancy Check algorithm is a simple error-checking method used in data communication to verify data integrity. It calculates a checksum by summing the byte values of the data and computing the two's complement of the sum (i.e., subtracting the sum from 256 modulo 256). This checksum, often presented in hexadecimal format, allows detection of errors in data transmission.

### How to Use the Source Code

- The implementation is provided in a pure C# library.
- Use the static method ComputeLRC in the LrcCalculator class to calculate the LRC of a given byte array.
- If the input data is null or empty, the method safely returns 0.
- Integrate the library into your project, reference it, and run the provided unit tests using MSTest.
- The console application allows you to input hex values separated by space to compute and display the LRC.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
  