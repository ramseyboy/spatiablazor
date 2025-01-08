#!/usr/bin/env pwsh

param (
    [string]$versionPart = 'minor',
    [Parameter(Mandatory=$true)][string]$branchName,
    [string]$buildNumber = '1'
)

function ParseVersion([Parameter()] $version) {
    $split = $version.Split('.')
    if ($split.Length -lt 3) {
        Throw New-Object System.ArgumentOutOfRangeException "Tagged version must be in the format v<major>.<minor>.<patch>-<prereleasetag>"
    }
    $major = [int]$split[0]

    $minor = [int]$split[1]
    if ($versionPart -eq 'minor') {
        $minor = $minor + 1
        $patch = 0
        return "${major}.${minor}.${patch}"
    }

    $patch = [int]$split[2]
    if ($versionPart -eq 'patch') {
        $patch = $patch + 1
        return "${major}.${minor}.${patch}"
    }
}

# get latest tag and set as version after stripping v and any prerelease tag
$version = git describe --abbrev=0 --tags | % {$_.replace('v', '')} | % {if ($_.Contains('-')) { $_.Substring(0, $_.IndexOf('-'))} else {$_}}

if ($version -eq $null) {
    $version = '1.0.0'
    Write-Host "No git tagged versions exist, setting to 1.0.0"
}

$branchVersion = $branchName.replace('v','')

$branchVersionSplit = $branchVersion.Split('.')
if ($branchVersionSplit.Length -lt 3) {
    if ($branchVersion -match "^[\d\.]+$") {
        $versionMajor = [int]$version.split(".")[0]
        $branchVersionMajor = [int]$branchVersion
        if ($branchVersionMajor -gt $versionMajor) {
            $version = "$branchVersionMajor.0.0"
        } elseif ($branchVersionMajor -lt $versionMajor) {
            Throw New-Object System.ArgumentOutOfRangeException "Cannot create a nuget package version for branch $branchVersionMajor when latest tag major version is $versionMajor"
        } else {
            $version = ParseVersion($version)
        }
    }
    elseif ($version -ne '1.0.0'){
        # branch name is not a valid semver or number and version is not set to default, auto parse last tag for version
        $version = ParseVersion($version)
    }
} else {
    $version = $branchVersion
}

# set prerelease tag to beta
$date = Get-Date -Format "yyyyMMdd"
$version =  "$version-beta.$date.$buildNumber"
Write-Host "Final version: $version"
echo "VERSION=$version" >> $env:GITHUB_ENV
exit 0