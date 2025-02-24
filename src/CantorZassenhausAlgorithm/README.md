# Cantor-Zassenhaus Algorithm Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Cantor-Zassenhaus algorithm is a probabilistic method for factoring polynomials over finite fields. It works by selecting random polynomials and computing their greatest common divisors to split the input polynomial into irreducible factors. The algorithm is particularly effective for square-free polynomials and is widely used in computational algebra for efficient polynomial factorization.

To use the SourceCode:
1. Open the solution file (CantorZassenhausAlgorithm.sln) in Visual Studio or via the dotnet CLI.
2. Build the project to compile the library.
3. Run the provided MSTest unit tests to verify the implementation.
4. Integrate the library into your own projects by invoking the CantorZassenhaus.Factor method, supplying your polynomial (modeled by the Polynomial class), to obtain its factors.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>