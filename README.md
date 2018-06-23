# Cognitive-LUIS-Programmatic
.NET SDK for [LUIS Programmatic APIs](https://westus.dev.cognitive.microsoft.com/docs/services/5890b47c39e2bb17b84a55ff)

[![License](https://img.shields.io/github/license/andreluizsecco/cognitive-luis-programmatic.svg)](LICENSE)
[![Issues open](https://img.shields.io/github/issues/andreluizsecco/cognitive-luis-programmatic.svg)](https://github.com/andreluizsecco/Cognitive-LUIS-Programmatic/)

Branch | Build status | Nuget package
-------|-------|--------------
master | [![Build status](https://ci.appveyor.com/api/projects/status/2ae2e5d0dsprpfjd/branch/master?svg=true)](https://ci.appveyor.com/project/andreluizsecco/cognitive-luis-programmatic)|[![NuGet](https://img.shields.io/nuget/v/Cognitive.LUIS.Programmatic.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/Cognitive.LUIS.Programmatic/)
dev | [![Build status](https://ci.appveyor.com/api/projects/status/qp882n6f5uutiaf8/branch/dev?svg=true)](https://ci.appveyor.com/project/andreluizsecco/cognitive-luis-programmatic-bv7gr)|-

## Dependencies
.NET Standard 2.0

Supported frameworks: [https://docs.microsoft.com/pt-br/dotnet/standard/net-standard](https://docs.microsoft.com/pt-br/dotnet/standard/net-standard)

## Installing / Getting started

This SDK is available through Nuget packages: [https://www.nuget.org/packages/Cognitive.LUIS.Programmatic](https://www.nuget.org/packages/Cognitive.LUIS.Programmatic)

#### Nuget
```
Install-Package Cognitive.LUIS.Programmatic
```

#### .NET CLI
```
dotnet add package Cognitive.LUIS.Programmatic
```
## How to Use

Check the our Wiki: [https://github.com/andreluizsecco/Cognitive-LUIS-Programmatic/wiki/get-started](https://github.com/andreluizsecco/Cognitive-LUIS-Programmatic/wiki/get-started)

## Roadmap

Check the complete roadmap: [https://github.com/andreluizsecco/Cognitive-LUIS-Programmatic/wiki/roadmap](https://github.com/andreluizsecco/Cognitive-LUIS-Programmatic/wiki/roadmap)

### Features (v1.1.0)
#### Apps, Intents and Entities
* Get all
* Get by id
* Get by name
* Add
* Rename
* Delete

#### Utterances (Examples)
* Add labeled example to the app
* Add a batch of labeled examples to the app
* Review labeled examples
* Delete example labels

#### Train
* Sends a training request
* Gets the training status of all models (intents and entities)
* Sends a training request and get final status

#### Publish
* Publish app

## Author

The Cognitive-LUIS-Programmatic was developed by [André Secco](http://andresecco.com.br) under the [MIT license](LICENSE).