# TestMiner
[![Mutation testing badge](https://img.shields.io/endpoint?style=for-the-badge&url=https%3A%2F%2Fbadge-api.stryker-mutator.io%2Fgithub.com%2Flyndychivs%2FTestMiner%2Fmaster)](https://dashboard.stryker-mutator.io/reports/github.com/lyndychivs/TestMiner/master)

## 🔭 High Level
Test Miner was designed to parse NUnit3 Test Reports and store the results into a Relational Database.

The Database would provide users with the means to historically track and monitor trends of Test Results.

![Test Miner Diagram](/Resources/TestMinerDiagram.png)

## Prerequisites
| Prerequisite        | Note |
| :---                | :--- |
| .NET8 SDK           | .NET8 or greater required.<br/>Check current .NET version `dotnet --version`.<br/>Download .NET8 [here](https://dotnet.microsoft.com/en-us/download/dotnet/8.0). |
| SQL Server          | The Database template can be found within this repository [here](TestMiner.Database). <br/>Download SQL Server Express [here](https://www.microsoft.com/en-us/sql-server/sql-server-downloads).<br/>You're on the hook to pay for the storage. 😂 |
| NUnit3 Test Results | The NUnit3 Test Results must be in XML format.<br/>Use `--result=TestResult.xml;format=nunit3` when executing tests. |

## 🖥️ Test Miner Console Application
Specify the following commands & arguments:

### 🛠️Command Line Usages
#### ⛏️ mine
Mine Test Report files to the Database.
```bash
mine [parameters]
```
##### Parameters
| Argument                          | Description | Default | Required |
| :---                              | :---        | :---    | :---     |
| `--reports <filePath>`            | File paths of the NUnit3 Test Report files to upload ("mine") to the Database.<br/>Can specify multiple file paths. | — | Yes |
| `--connection <connectionString>` | The Connection String to the Database. | — | No |

###### Example
```bash
TestMiner.exe mine --reports "C:\SampleData\TestResults1.xml" --connection "Server=localhost\\SQLEXPRESS;Database=TestMiner;"
```

#### 💾 save
Saves the Database Connection String locally.
```bash
save [parameters]
```
##### Parameters
| Argument                          | Description | Default | Required |
| :---                              | :---        | :---    | :---     |
| `--connection <connectionString>` | The Connection String to the Database. | — | Yes |

###### Example
```bash
TestMiner.exe save --connection "Server=localhost\\SQLEXPRESS;Database=TestMiner;"
```

## Database
The Database project exists at [TestMiner.Database](TestMiner.Database); you can publish this Database to your own SQL Server instance.

### SQL Server in Docker
In case any guidance changes; [here](https://learn.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker) is a link to the Microsoft documentation for installing SQL Server on Docker.

*Pull the SQL Server 2022 (16.x) Linux container image from the Microsoft Container Registry.*
```bash
docker pull mcr.microsoft.com/mssql/server:2022-latest
```

*Run the Linux container image with Docker, use the following command from a bash shell or elevated PowerShell command prompt.*
```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=TestMinerPass1!" -p 1433:1433 --name testminer --hostname testminer -d mcr.microsoft.com/mssql/server:2022-latest
```

> [!CAUTION]
> When you stop and remove a container, your SQL Server data in the container is permanently deleted.

*View the Docker containers:*
```bash
docker ps -a
```

*Example output:*
```bash
CONTAINER ID   IMAGE                                        COMMAND                    CREATED         STATUS         PORTS                                       NAMES
d4a1999ef83e   mcr.microsoft.com/mssql/server:2022-latest   "/opt/mssql/bin/perm..."   2 minutes ago   Up 2 minutes   0.0.0.0:1433->1433/tcp, :::1433->1433/tcp   testminer
```

You should now be able to connect to the SQL Server, using `localhost` on port `1433` with the username `sa` and password `TestMinerPass1!`

*Connection String Example:*
```
Data Source=localhost,1433;Database=TestMiner;User ID=sa;Password=TestMinerPass1!;Encrypt=true;TrustServerCertificate=true;
```

Using Visual Studio, Publish [TestMiner.Database](TestMiner.Database) to the the Docker container.

## Testing
- Unit Testing
  - [TestMiner.Tests](TestMiner.Tests)
  - [TestMiner.Models.Tests](TestMiner.Models.Tests)
- Component Testing
  - [TestMiner.Database.ComponentTests](TestMiner.Database.ComponentTests)
- Integration Testing
  - [TestMiner.IntegrationTests](TestMiner.IntegrationTests)
- Mutation Testing
  - [Strkyer.NET](https://dashboard.stryker-mutator.io/reports/github.com/lyndychivs/TestMiner/master) with [my GitHub Action](https://github.com/lyndychivs/dotnet-stryker-action)
