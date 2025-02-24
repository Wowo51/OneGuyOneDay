#ChsConversion Algorithm Overview

A pure C# library. No external dependencies except for Microsoft's unit testing. No binaries. Unit tested.

This code is free to use under the MIT.

The ChsConversion algorithm provides conversion functions for disk addressing. It converts a Cylinder-Head-Sector (CHS) formatted address to a Logical Block Address (LBA) and vice versa. The CHS to LBA conversion multiplies the cylinder number by the number of heads per cylinder, adds the head number, multiplies by the number of sectors per track, and then adds the sector offset (adjusted for 1-based numbering). The reverse conversion uses division and modulus to extract the cylinder, head, and sector from an LBA value.

To use the SourceCode, integrate the library into your C# project by referencing the ChsConversion project or its compiled DLL. Invoke the static methods ChsToLba to compute the LBA from given CHS parameters, or LbaToChs to retrieve the CHS values from an LBA. The included unit tests provide usage examples and verify the correctness of the conversions.

![AI Image](aiimage.jpg)
[TranscendAI.tech](https://TranscendAI.tech)<br>
<br>
Copyright [TranscendAI.tech](https://TranscendAI.tech) 2025.</br>
Authored by Warren Harding. AI assisted.