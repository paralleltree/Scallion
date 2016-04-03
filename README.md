# Scallion
A wrapper library for MikuMikuDance

[![Build status](https://ci.appveyor.com/api/projects/status/lu3jua55lgs0pv05?svg=true)](https://ci.appveyor.com/project/paralleltree/scallion)
[![NuGet Release](https://img.shields.io/nuget/vpre/Scallion.svg)](https://www.nuget.org/packages/Scallion)
[![Codecov](https://img.shields.io/codecov/c/github/paralleltree/Scallion.svg)](https://codecov.io/github/paralleltree/Scallion)


## Requirements
  * .NET Framework 4.6
  * System.Numerics.Vectors

## Installation
Now available on [NuGet](https://www.nuget.org/packages/Scallion)

You can execute following command to install:

```
PM> Install-Package Scallion -IncludePrerelease
```

Or download an archive from [Releases](https://github.com/paralleltree/Scallion/releases).

## Usage

This library supports only MMD Motion File for now.

```csharp
// using Scallion.DomainModels;

var motion = new Motion().Load(@"path\to\motion.vmd");
// motion.Bones...
// motion.Morphs...

```

## Contributing

Bug reports and pull requests are welcome on [GitHub](https://github.com/paralleltree/spcms).

## License

The project is available as open source under the terms of the [MIT License](http://opensource.org/licenses/MIT).
