
# Temporal Difference Learning

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Temporal Difference Learning is a reinforcement learning algorithm that estimates value functions based on the difference between consecutive predictions and received rewards. This approach updates the value of a state by comparing the estimated value and the reward received from transitioning to the next state. The included C# implementation provides a TDLearner class, which you can initialize with a learning rate (alpha) and a discount factor (gamma). To use the source code, instantiate TDLearner, then call the Update method with the current state, next state, and reward. Retrieve the estimated value for any state using the GetValue method. The accompanying unit tests demonstrate typical usage patterns and validate the behavior of the algorithm.
 
![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.
  