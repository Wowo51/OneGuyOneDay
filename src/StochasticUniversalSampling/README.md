# StochasticUniversalSampling Documentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Stochastic Universal Sampling (SUS) algorithm is an efficient selection strategy used in evolutionary computation and genetic algorithms. It works by first calculating the total fitness of the entire population and then dividing this total by the number of selections required to determine equally spaced pointers. Starting from a random offset within the first interval, these pointers systematically sweep through the population, selecting individuals proportionally to their fitness values. This method ensures a more even distribution in the selection process compared to traditional stochastic or roulette wheel methods.

To use the SourceCode, simply add a reference to the compiled library in your C# project. Call the static method Select<T> from the StochasticUniversalSampler class by passing in your population and a fitness evaluation function. The algorithm will automatically handle cases where fitness values are zero or negative by falling back to uniform selection. Unit tests provided in the repository demonstrate various scenarios, making it straightforward to integrate and validate the algorithm in your own projects.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>