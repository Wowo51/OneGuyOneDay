# Minimax Algorithm Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Minimax algorithm is a classic decision-making technique used primarily in two-player, turn-based games. It works by recursively simulating all possible moves, evaluating each resulting game state, and selecting the move that minimizes the potential maximum loss. This approach assumes that the opponent will also play optimally, leading to a balanced minimax decision process.

To use this SourceCode, add the library to your C# project and implement the IGameState interface for your game. Instantiate the MinimaxSolver class and call its Minimax method by providing a valid game state, a flag to indicate whether the current move is maximizing or minimizing, and the desired search depth. Review and run the accompanying MSTest unit tests to verify its behavior and to gain insight into integrating the algorithm into your project.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>