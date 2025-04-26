# 💽 TestMiner.Database
## Prerequisites
| Prerequisite         | Note |
| :---                 | :--- |
| Microsoft SqlPackage | SqlPackage is required to publish the Database (Visual Studio can also) - Download [here](https://learn.microsoft.com/en-us/sql/tools/sqlpackage/sqlpackage) |
| Docker               | Docker is required for local testing and validation, it is also possible to use an (local or remote) instance of Sql Server. |

## Hosting Sql Server using Docker
When you're ready, navigate to `TestMiner/TestMiner.Database` directory.

```bash
docker compose up --build -d
```

Execute the following command until the `Status` reports `healthy`
```bash
docker ps -a
```
Example:
```bash
CONTAINER ID   IMAGE                                        COMMAND                  CREATED         STATUS                   PORTS                    NAMES
95fd8c61886e   mcr.microsoft.com/mssql/server:2022-latest   "/opt/mssql/bin/laun…"   9 seconds ago   Up 9 seconds (healthy)   0.0.0.0:1433->1433/tcp   testminer
```

## Publish TestMiner Database to Docker
Once the Docker instance is deployed and healthy, using PowerShell execute:
```ps1
.\DeployToDocker.ps1
```
The following result should be present.
```bash
TestMiner deployed to localhost,1433
```

You should now be able to connect to the SQL Server, using `localhost` on port `1433` with the username `sa` and password `TestMinerPass1!`
*Connection String Example:*
```
Data Source=localhost,1433;Database=TestMiner;User ID=sa;Password=TestMinerPass1!;Encrypt=true;TrustServerCertificate=true;
```

> [!CAUTION]
> When you stop and remove a container, your SQL Server data in the container is permanently deleted.