
# Delta Encoding Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Delta Encoding algorithm transforms a sequence of integer values into a series of differences between consecutive elements. The first element is retained, and each subsequent element is replaced by the difference from its predecessor. This method is particularly useful for compressing data when consecutive values are similar.

To use the SourceCode:
1. Add a reference to the DeltaEncoding project in your solution.
2. Call the static methods DeltaEncoder.Encode to compute the differences and DeltaEncoder.Decode to reconstruct the original sequence.
3. Run the provided MSTest unit tests to verify functionality.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
  