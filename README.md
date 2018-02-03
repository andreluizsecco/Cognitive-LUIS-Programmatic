# Cognitive-LUIS-Programmatic
.NET SDK for [LUIS Programmatic APIs](https://westus.dev.cognitive.microsoft.com/docs/services/5890b47c39e2bb17b84a55ff)

[![Build status](https://ci.appveyor.com/api/projects/status/2ae2e5d0dsprpfjd?svg=true)](https://ci.appveyor.com/project/andreluizsecco/cognitive-luis-programmatic)
[![License](https://img.shields.io/github/license/andreluizsecco/cognitive-luis-programmatic.svg)](LICENSE)
[![Issues open](https://img.shields.io/github/issues/andreluizsecco/cognitive-luis-programmatic.svg)](https://github.com/andreluizsecco/Cognitive-LUIS-Programmatic/)

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

### Implemented Features (v1.0.4)
#### Apps, Intents and Entities
* Get all
* Get by id
* Get by name
* Add
* Rename
* Delete

#### Utterances (Examples)
* Add labeled example to the app

#### Train
* Sends a training request
* Gets the training status of all models (intents and entities)

#### Publish
* Publish app

## Author

The Cognitive-LUIS-Programmatic was developed by [André Secco](http://andresecco.com.br) under the [MIT license](LICENSE).
