# AlphaBetaPruning Library Overview

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

Alpha-beta pruning is a search algorithm that optimizes the minimax technique by eliminating branches that cannot influence the final decision. It works by maintaining two thresholds—alpha for the maximizer and beta for the minimizer—and pruning any branch where the result would not affect the outcome. This results in significant performance improvements by reducing the number of nodes evaluated.

To use the SourceCode:
1. Integrate the provided C# library into your project.
2. Implement the IGameTreeNode interface to represent your game tree.
3. Use the AlphaBetaPruner class to perform decision making using the alpha-beta pruning algorithm.
4. Run the included MSTest unit tests to verify behavior and ensure correct integration.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.