
# Locality Sensitive Hashing Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Locality Sensitive Hashing (LSH) is an algorithm used for dimension reduction and efficient similarity search. It works by projecting vectors onto random hyperplanes and generating a binary signature based on the result of each projection. Each bit in the signature indicates whether the vector lies on the positive or negative side of a corresponding hyperplane, enabling fast approximations of similarity in high-dimensional data.

To use the SourceCode, simply add a reference to the LocalitySensitiveHashing project in your solution. Create an instance of the LSH class by specifying the number of dimensions and the desired hash size. Then, call the ComputeSignature method with a properly sized vector to obtain its binary signature. The included unit tests in the LocalitySensitiveHashingTest project demonstrate various scenarios—from edge cases such as null or incorrectly sized vectors to typical and large vectors—ensuring the algorithm's reliability.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
  