# DaitchMokotoff Soundex Algorithm

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The Daitch–Mokotoff Soundex algorithm is designed to index surnames based on their phonetic pronunciation, particularly for Slavic and Germanic names. It normalizes diacritics and applies recursive encoding rules to generate one or more six-digit codes that represent the sound of a surname. Special sequences like "SCH" and "DZ" are handled with alternative mappings to capture varying pronunciations.

To use the SourceCode:
- Integrate the library into your C# project.
- Call the static Encode method from the DaitchMokotoffSoundex class with a surname as input. This method returns an array of six-digit codes representing different phonetic interpretations.
- Refer to the accompanying MSTest unit tests for examples of how various name cases are handled.
- Ensure your project targets .NET 9.0 and has the required MSTest setup for unit testing.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.