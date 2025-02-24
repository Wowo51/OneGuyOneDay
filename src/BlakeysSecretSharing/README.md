
# Blakeys Secret Sharing Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

Blakey's Secret Sharing algorithm is a method for dividing a secret into multiple shares using hyperplanes in a multi-dimensional space. Each share is represented by a hyperplane whose coefficients and constant are derived from the secret. The secret can only be reconstructed when a sufficient number of shares—equal to the dimension of the secret—are combined using linear algebra techniques such as Gaussian elimination.

To use the SourceCode provided:
1. Include the BlakeysSecretSharing project in your C# solution.
2. Call the GenerateShares method with your secret (provided as an array of double values) and the desired share count to generate hyperplane shares.
3. Use the ReconstructSecret method by supplying a collection of shares to recover the original secret. Ensure that the number of shares meets or exceeds the secret's dimension for a successful reconstruction.
4. Refer to the comprehensive unit tests (using Microsoft's unit testing framework) included in the repository, which serve both to verify functionality and to provide usage examples.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
  