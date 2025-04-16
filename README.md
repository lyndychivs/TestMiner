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
The Database project exists at [TestMiner.Database](TestMiner.Database); you can publish this Database to your own SQL Server instance.

### SQL Server in Docker
Incase any guidance changes; [here](https://learn.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker) is a link to the Microsoft documentation for installing SQL Server on Docker.

Pull the SQL Server 2022 (16.x) Linux container image from the Microsoft Container Registry.
```bash
docker pull mcr.microsoft.com/mssql/server:2022-latest
```

To run the Linux container image with Docker, you can use the following command from a bash shell or elevated PowerShell command prompt.
```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=TestMinerPass1!" -p 1433:1433 --name testminer --hostname testminer -d mcr.microsoft.com/mssql/server:2022-latest
```
> [!CAUTION]
> When you stop and remove a container, your SQL Server data in the container is permanently deleted.

View the Docker containers:
```bash
docker ps -a
```
Example output:
```bash
CONTAINER ID   IMAGE                                        COMMAND                    CREATED         STATUS         PORTS                                       NAMES
d4a1999ef83e   mcr.microsoft.com/mssql/server:2022-latest   "/opt/mssql/bin/perm..."   2 minutes ago   Up 2 minutes   0.0.0.0:1433->1433/tcp, :::1433->1433/tcp   testminer
```

You should now be able to connect to the SQL Server, using `localhost` on port `1433` with the username `sa` and password `TestMinerPass1!`

[Publish](https://learn.microsoft.com/en-us/sql/tools/sql-database-projects/get-started?view=sql-server-ver16&pivots=sq1-visual-studio#step-4-deploy-the-project) [TestMiner.Database](TestMiner.Database) to the the Docker container.

## Testing
- Unit Testing
  - [TestMiner.Tests](TestMiner.Tests)
  - [TestMiner.Models.Tests](TestMiner.Models.Tests)
- Integration Testing
  - [TestMiner.Database.IntegrationTests](TestMiner.Database.IntegrationTests)
- Mutation Testing
  - [Strkyer.NET](https://dashboard.stryker-mutator.io/reports/github.com/lyndychivs/TestMiner/master) with [my GitHub Action](https://github.com/lyndychivs/dotnet-stryker-action)
