# Railflow.NUnit.TestRail.Reporter Examples

This repository contains examples on how to use [Railflow.NUnit.TestRail.Reporter](https://www.nuget.org/packages/Railflow.NUnit.TestRail.Reporter/) package with NUnit tests for TestRail integration.

Please refer to full documentation on how to integrate [NUnit](https://nunit.org/) with [TestRail](https://www.gurock.com/testrail/) on our [website](https://docs.railflow.io/docs/railflow-for-testrail/testing-frameworks/nunit)

## Prerequisites

* .NET Framework 4.5 or newer
* NuGet
* NUnit 3 
* Visual Studio 2017

## Running tests

Use [NUnit.ConsoleRunner](https://www.nuget.org/packages/NUnit.ConsoleRunner/) or [dotnet CLI](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-test) to run examples and generate report XML.

E.g.

```powershell
nunit3-console.exe ./Example/bin/debug/net472/Example.dll 
```

OR

```powershell
dotnet test ./Example.csproj --logger:nunit -- NUnit.TestOutputXml=../../../TestResults
```

**NOTE:** [NunitXml.TestLogger](https://www.nuget.org/packages/NunitXml.TestLogger/) NuGet package should be installed in order to use "NUnit" XML logger with **dotnet CLI**
