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
dotnet test ./Example.csproj --logger:nunit -- NUnit.TestOutputXml=./TestResults
```

**NOTE:** [NunitXml.TestLogger](https://www.nuget.org/packages/NunitXml.TestLogger/) NuGet package should be installed in order to use "NUnit" XML logger with **dotnet CLI**

## Exporting results into TestRail

Installing Railflow NPM CLI:

```shell
npm install railflow
```

Exporting data into TestRail:

```shell
npx railflow -k ABCDE-12345-FGHIJ-67890 -url https://testrail.your-server.com/ -u testrail-username -p testrail-password -pr "Railflow Demo" -path section1/section2 -f nunit -r TestResult.xml -sm path
```

Where:

| Key                | Description                                                                                                                                                                                                                                                                                                                                                                                                                                         |
|--------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| -k, --key          | (online activation) License key. Can be set with RAILFLOW_LICENSE environment variable                                                                                                                                                                                                                                                                                                                                                              |
| -url, --url        | TestRail instance URL                                                                                                                                                                                                                                                                                                                                                                                                                               |
| -u, --username     | TestRail username.                                                                                                                                                                                                                                                                                                                                                                                                                                  |
| -p, --password     | TestRail password or API Key.                                                                                                                                                                                                                                                                                                                                                                                                                       |
| -pr, --project     | TestRail project name                                                                                                                                                                                                                                                                                                                                                                                                                               |
| -path, --test-path | TestRail test cases path                                                                                                                                                                                                                                                                                                                                                                                                                            |
| -f, --format       | Report format: 'nunit' (case insensitive)                                                                                                                                                                                                                                                                                                                                                                                                           |
| -r, --report-files | The file path(s) to the test report file(s) generated during the build.                                                                                                                                                                                                                                                                                                                                                                             |
| -sm, --search-mode | Specifies the test case lookup algorithm. <br/> `name:` search for test case matching the name within the entire test suite. If test case found, update the test case. If test case not found, create a new test case within specified `-path` <br/> `path:` search for test case matching the name within the specified `-path`. If test case found, update the test case. If test case not found, create a new test case within specified `-path` |

Please see [Railflow NPM documentation](https://docs.railflow.io/docs/railflow-for-testrail/railflow-cli/cli-reference) for further details.

