
#FitnessProportionateSelection Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

FitnessProportionateSelection is an algorithm implemented as a C# library that performs roulette-wheel selection for a given population. The algorithm calculates the total sum of fitness values across the population, then picks a random threshold and iterates through the population accumulating fitness, selecting the individual where the cumulative fitness meets or exceeds the threshold. This technique is especially useful in genetic algorithms and other evolutionary computation scenarios where selection probability is proportional to fitness.

To use the source code, include the FitnessProportionateSelection library in your project. Simply call the static method 'Select' from the RouletteWheelSelector class by passing a collection of individuals, a function that computes each individual's fitness, and a Random instance to generate the threshold. The source code comes with comprehensive unit tests using MSTest, ensuring that both typical and edge cases are handled correctly.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.
Authored by Warren Harding. AI assisted.
  