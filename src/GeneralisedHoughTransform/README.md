# Generalised Hough Transform

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Generalised Hough Transform algorithm detects shapes in images by analyzing gradients computed from a template. The algorithm first calculates the horizontal and vertical differences in pixel intensity to determine edge strength and orientation. It then quantizes these angles and uses them to vote in an accumulator matrix that highlights areas matching the template features.

To use the SourceCode, instantiate the library, set a template via the SetTemplate method, and then call Detect on your image. The unit tests demonstrate various scenarios including handling of null inputs, basic template matching, edge threshold detection, and more. This modular approach makes it easy to integrate into your project for object detection and image analysis.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>