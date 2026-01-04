FROM mcr.microsoft.com/mssql/server:2022-latest

USER root

RUN apt-get update && \
    apt-get install -y wget unzip && \
    wget -q https://aka.ms/sqlpackage-linux -O sqlpackage.zip && \
    unzip -q sqlpackage.zip -d /opt/sqlpackage && \
    chmod +x /opt/sqlpackage/sqlpackage && \
    rm sqlpackage.zip && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

COPY bin/Release/TestMiner.dacpac /tmp/TestMiner.dacpac

COPY entrypoint.sh /tmp/entrypoint.sh

RUN chmod +x /tmp/entrypoint.sh

USER mssql

ENTRYPOINT ["/tmp/entrypoint.sh"]
