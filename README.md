# TestMiner
[![Mutation testing badge](https://img.shields.io/endpoint?style=for-the-badge&url=https%3A%2F%2Fbadge-api.stryker-mutator.io%2Fgithub.com%2Flyndychivs%2FTestMiner%2Fmaster)](https://dashboard.stryker-mutator.io/reports/github.com/lyndychivs/TestMiner/master)

## High Level
Test Miner was designed to parse NUnit3 Test Reports and store the information into a Relational Database.

The Database would provide users with the means to historically track and monitor trends of Test Results.

![Test Miner Diagram](/Resources/TestMinerDiagram.png)

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (you're on the hook to pay for the storage 😂)
- NUnit3 Test Results in XML format (use `--result=TestResult.xml;format=nunit3` when running tests).

## Test Miner Console Application
Specify the following commands & arguments:

#### `--reports` or `-r` (required)
File paths of the NUnit3 Test Report files to process.
##### Example:
```sh
TestMiner.exe --reports "C:\SampleData\TestResults1.xml"
```
or multiple reports:
```sh
TestMiner.exe --reports "C:\SampleData\TestResults1.xml" "C:\SampleData\TestResults2.xml"
```

#### `--connection` or `-c` (optional)
The Connection String to the Database.
##### Example:
```sh
TestMiner.exe --reports "C:\SampleData\TestResults1.xml" --connection "Server=localhost\\SQLEXPRESS;Database=TestMiner;"
```
It's also possible to specify the Database Connection string via the [Connection.json](TestMiner/Connection.json) file.

## Database
The Database project exists at [TestMiner.Database](TestMiner.Database); you can publish this Database to any SQL Express Server instance.

I plan on implementing a docker container for this also.

## Testing
- Unit Testing
  - [TestMiner.Tests](TestMiner.Tests)
  - [TestMiner.Models.Tests](TestMiner.Models.Tests)
- Integration Testing
  - [TestMiner.Database.IntegrationTests](TestMiner.Database.IntegrationTests)
- Mutation Testing
  - [Strkyer.NET](https://dashboard.stryker-mutator.io/reports/github.com/lyndychivs/TestMiner/master) with [my GitHub Action](https://github.com/lyndychivs/dotnet-stryker-action)