#!/bin/bash
/opt/mssql/bin/sqlservr &

echo "Waiting for SQL Server to start..."
sleep 30s

echo "Deploying TestMiner database..."
/opt/sqlpackage/sqlpackage /Action:Publish \
    /SourceFile:/tmp/TestMiner.dacpac \
    /TargetServerName:localhost \
    /TargetDatabaseName:TestMiner \
    /TargetUser:SA \
    /TargetPassword:${MSSQL_SA_PASSWORD} \
    /TargetTrustServerCertificate:True

if [ $? -eq 0 ]; then
    echo "TestMiner.Database deployed successfully!"
else
    echo "TestMiner.Database deployment failed!"
fi

wait
