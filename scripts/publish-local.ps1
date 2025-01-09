#!/usr/bin/env pwsh

param (
    [Parameter(Mandatory=$true)][string]$localNugetPath
)

$script = $PSScriptRoot + "/set-beta-version.ps1"
$buildNumber = [math]::Round(((get-date) - (get-date -Hour 0 -Minute 00 -Second 00)).TotalSeconds)
$version = & $script -branchName main -buildNumber $buildNumber 

dotnet pack ./*.sln --configuration Release /p:Version=$version --output pack/

nuget init pack/ $localNugetPath