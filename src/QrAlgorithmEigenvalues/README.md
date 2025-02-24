# QR Algorithm Eigenvalues

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

The QR Algorithm is an iterative method used for computing the eigenvalues and, optionally, the eigenvectors of a square matrix. It works by repeatedly performing a QR decomposition on the matrix and updating it until the off-diagonal elements are negligibly small, at which point the diagonal elements approximate the eigenvalues. 

To use the source code, include the QrAlgorithmEigenvalues project in your solution and add a reference to it in your application. Invoke the static method Compute from the QRAlgorithm class by passing a square matrix as a two-dimensional double array. Set the second parameter to true if you wish to compute the eigenvectors as well. Detailed unit tests provided in the repository can serve as examples for correct usage.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>