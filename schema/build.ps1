avrogen.exe -s .\avro\user.avsc .
$version = (Get-Content .\version.txt) -as [int]
$version++

Out-File -FilePath ./version.txt -Encoding ASCII -InputObject $version

dotnet build --configuration Release /p:version=1.$version.0
dotnet nuget push -s http://localhost:5555/v3/index.json -k NUGET-SERVER-API-KEY .\bin\Release\Timvw.Avro.1.$version.0.nupkg