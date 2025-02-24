# RelevanceVectorMachine Overview

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

RelevanceVectorMachine is a machine learning algorithm that performs classification using a sparse Bayesian learning approach. It utilizes a Gaussian kernel to measure similarity between data points and iteratively adjusts weights and the bias through gradient descent to optimize predictions. Unlike traditional SVMs, it produces probabilistic outputs that provide insight into prediction confidence.

To use the SourceCode, add the library to your C# project. Prepare your training data as a two-dimensional array of doubles along with an array of target labels, then invoke the Train method on an instance of RelevanceVectorMachine. After training, use the PredictProbability method to obtain a probability score for new inputs. The provided unit tests demonstrate common usage patterns, including handling null inputs and validating input dimensions. Detailed inline documentation in the source further explains customization and integration.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>