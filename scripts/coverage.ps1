#!/usr/bin/env pwsh

dotnet tool install -g dotnet-reportgenerator-globaltool

#rm -r "${$PSScriptRoot}SpatiaBlazor.Tests/TestResults"

dotnet test --logger trx --collect:"XPlat Code Coverage" --settings ./coverlet.runsettings

$cobertura = dir "${$PSScriptRoot}SpatiaBlazor.Tests/TestResults" | Sort-Object LastAccessTime | Where-Object {$_.PSIsContainer} | Select-Object -First 1  -ExpandProperty FullName FullName

reportgenerator -reports:"$cobertura/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html -sourcedirs:"${$PSScriptRoot}"

Start-Process "coveragereport/index.html"