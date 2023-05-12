# TDP

In order to deploy SQL Server image in docker run the following bash command:

"docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=tdp2022$" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest"
