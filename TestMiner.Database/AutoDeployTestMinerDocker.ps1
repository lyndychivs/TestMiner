# Ensure the script stops on errors
Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

docker compose up --build -d

.\DeployToDocker.ps1