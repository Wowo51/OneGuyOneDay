# ID3Algorithm Overview

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

ID3Algorithm is an implementation of the Iterative Dichotomiser 3 algorithm that builds decision trees by recursively selecting the attribute with the highest information gain. The algorithm stops splitting when all examples share the same label or when no further informative attributes remain. 

To use the source code, include the library in your project, prepare your dataset of examples with associated attributes and labels, and invoke the BuildTree method with your chosen list of attributes. The provided unit tests demonstrate how to format your input data and validate the resulting decision tree.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.