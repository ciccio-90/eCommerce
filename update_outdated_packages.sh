#!/bin/bash
dotnet tool update --global dotnet-outdated-tool
cd "$(dirname "$0")"
dotnet outdated -u