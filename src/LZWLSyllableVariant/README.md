
# LZWLSyllableVariant Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The LZWLSyllableVariant algorithm is a custom variant of the classic LZW compression algorithm tailored to work on syllables rather than individual characters. It first splits the input text into syllables using a vowel-based heuristic, then dynamically builds a dictionary of syllable sequences to compress the text. This approach can yield efficient compression for languages where syllable structures carry significant meaning.

Usage Instructions:
1. Compression: Invoke SyllableLzw.Compress with your input string. This method returns a CompressionResult that contains a list of integer codes and an initial dictionary mapping.
2. Decompression: Use SyllableLzw.Decompress with the CompressionResult to reconstruct the original text.
3. Testing: The solution includes thorough MSTest unit tests. Run these tests in Visual Studio or with the command line using 'dotnet test' to ensure proper functionality.

The provided SourceCode package includes all necessary project files, source implementations, and test cases to integrate and verify the algorithm in your own projects.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
