avrogen.exe -s .\avro\user.avsc .
dotnet build --configuration Release
# dotnet nuget push -s http://localhost:5555/v3/index.json -k NUGET-SERVER-API-KEY .\bin\Release\myDTO.Avro.1.0.1.4506.nupkg