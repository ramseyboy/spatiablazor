#!/usr/bin/env pwsh

$version = git describe --abbrev=0 --tags | % {$_.replace('v', '')}
echo "VERSION=$version" >> $env:GITHUB_ENV
exit 0