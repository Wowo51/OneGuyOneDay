
# Warped Predictive Coding Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

The WarpedPredictiveCoding algorithm is a specialized form of linear predictive coding that incorporates a warping factor to adjust the analysis of signal characteristics. It first applies a warping transformation to the input signal, calculates its autocorrelation, and then uses the Levinson-Durbin recursion to compute the prediction coefficients. This approach is particularly useful in speech processing and audio analysis where subtle nuances in spectral properties are significant.

To use the provided SourceCode, include the WarpedPredictiveCoding library in your project by referencing its project file or compiled DLL. Utilize the ComputeCoefficients method by passing a valid signal array, a desired prediction order, and an appropriate warp factor. The accompanying unit tests illustrate common usage scenarios and edge cases, ensuring reliable performance. For further customization and debugging, refer to the detailed logs embedded within the record files.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
  