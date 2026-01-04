# 💽 TestMiner.Database
## Prerequisites
| Prerequisite         | Note |
| :---                 | :--- |
| Docker               | Docker is required for local testing and validation, it is also possible to use a (local or remote) instance of SQL Server. |

## Setup and Deployment

### Step 1: Build the Database Project
Before running Docker, build the database project to generate the DACPAC file:

```bash
msbuild TestMiner.Database.sqlproj /p:Configuration=Release
```

This creates `TestMiner.dacpac` in the `bin/Release/` directory, which will be automatically deployed when the container starts.

### Step 2: Configure Environment Variables
Create a `.env` file in the `TestMiner.Database` directory from the template:

```bash
cp .env.template .env
```

Edit `.env` to set your SA password (default is `Password1!`):
```dotenv
MSSQL_SA_PASSWORD=YourSecurePassword123!
```

> [!WARNING]
> The password must meet SQL Server complexity requirements (at least 8 characters, including uppercase, lowercase, numbers, and symbols).

### Step 3: Start the Container
Navigate to the `TestMiner.Database` directory and run:

```bash
docker compose up --build -d
```

The container will:
1. Start SQL Server 2022 Express
2. Automatically deploy the TestMiner database using the DACPAC
3. Run health checks to ensure it's ready

### Step 4: Verify Deployment
Check the container status:
```bash
docker ps -a
```

Example output:
```bash
CONTAINER ID   IMAGE                   COMMAND                  CREATED          STATUS                    PORTS                    NAMES
95fd8c61886e   testminer-db:latest    "/tmp/entrypoint.sh"     20 seconds ago   Up 18 seconds (healthy)   0.0.0.0:1433->1433/tcp   testminer
```

View deployment logs:
```bash
docker logs testminer
```

Look for the message: `TestMiner.Database deployed successfully!`

## Connecting to the Database
You can now connect to SQL Server using:
- **Host:** `localhost`
- **Port:** `1433`
- **Username:** `sa`
- **Password:** (value from your `.env` file)
- **Database:** `TestMiner`

**Connection String Example:**
```
Data Source=localhost,1433;Database=TestMiner;User ID=sa;Password=Password1!;Encrypt=true;TrustServerCertificate=true;
```

## Managing the Container

**Stop the container:**
```bash
docker compose down
```

**Rebuild and redeploy:**
```bash
msbuild TestMiner.Database.sqlproj /p:Configuration=Release
docker compose up --build -d
```

**View container logs:**
```bash
docker logs testminer -f
```

> [!CAUTION]
> When you stop and remove a container with `docker compose down`, your SQL Server data in the container is permanently deleted. For persistent data, consider adding a volume mount in `compose.yaml`.