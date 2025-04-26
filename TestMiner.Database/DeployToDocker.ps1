$dacpacPath = ".\bin\Release\TestMiner.dacpac"
$serverName = "localhost,1433"
$databaseName = "TestMiner"
$sqlUser = "SA"
$sqlPassword = "TestMinerPass1!"

SqlPackage /Action:Publish `
           /SourceFile:$dacpacPath `
           /TargetServerName:$serverName `
           /TargetDatabaseName:$databaseName `
           /TargetUser:$sqlUser `
           /TargetPassword:$sqlPassword `
           /TargetTrustServerCertificate:True

if ($LASTEXITCODE -eq 0) {
    Write-Host "$databaseName deployed to $serverName"
} else {
    Write-Host "$databaseName deployment failed with exit code: $LASTEXITCODE"
}