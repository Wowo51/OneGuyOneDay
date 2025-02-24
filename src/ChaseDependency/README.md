# Chase Dependency Library

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The ChaseDependency algorithm implements the chase procedure used in database theory for dependency checking. It works by taking an initial database of facts (atoms) and a set of dependencies, then iteratively applying these dependencies to infer new facts until no additional facts can be generated. The algorithm uses unification to match the antecedents of dependencies with the existing atoms, then applies substitutions to produce the consequents when the dependency is applicable.

To use the SourceCode, compile the solution in a compatible .NET environment (targeting net9.0). The repository includes the main library and a set of unit tests using MSTest to validate the functionality. The sample tests—covering scenarios such as simple chase operations, chained dependencies, and large database processing—provide guidance for integrating and extending the library.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.</br>
