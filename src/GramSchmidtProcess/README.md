#GramSchmidt Process Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Gram-Schmidt process is an algorithm that takes a set of linearly independent vectors and transforms them into a set of orthogonal (or orthonormal) vectors in an inner product space. It works by iteratively subtracting the projections of each vector onto the previously computed orthogonal vectors, ensuring that each new vector is perpendicular to all those that came before it.

How to use the SourceCode:
1. Include the GramSchmidtProcess project in your C# solution.
2. Reference the library in your application.
3. Call Orthogonalizer.Orthogonalize to obtain an orthogonal set of vectors, or Orthogonalizer.Orthonormalize to get an orthonormal set.
4. Run the provided unit tests in the GramSchmidtProcessTest project to see usage examples and confirm that the implementation behaves as expected.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>