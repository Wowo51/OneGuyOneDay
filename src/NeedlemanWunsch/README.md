# NeedlemanWunsch Help

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Needleman-Wunsch algorithm is a dynamic programming algorithm for performing global sequence alignment between two sequences. It builds a scoring matrix based on defined penalties for gaps, mismatches, and rewards for matches. After populating the matrix, the algorithm uses a traceback process to determine the optimal alignment path, resulting in two sequences aligned with the best possible score. This method is widely used in bioinformatics for comparing DNA, RNA, or protein sequences.

To use the SourceCode:
1. Compile the library using .NET (target framework net9.0).
2. Run the unit tests provided with Microsoft's testing framework to verify correctness.
3. Execute the Main program to see a sample run aligning "GATTACA" with "GCATGCU".
4. Adjust the scoring parameters (match, mismatch, gap) as needed for your specific alignment requirements.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.