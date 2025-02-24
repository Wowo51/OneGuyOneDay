#ChaffAlgorithmDocumentation

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

Chaff Algorithm is a SAT solver that applies a backtracking search strategy to efficiently determine satisfying assignments for boolean formulas expressed in conjunctive normal form. The algorithm recursively assigns truth values to variables and detects conflicts early, pruning the search space in the process.

To use the SourceCode, compile the project with .NET 9.0 and run the executable to solve sample CNF inputs. You can also integrate the ChaffSolver class into your own project. Comprehensive unit tests using MSTest are provided to validate various scenarios, including simple, large, and unsatisfiable clause sets.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>