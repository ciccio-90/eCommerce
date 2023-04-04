#!/bin/bash
dotnet restore
dotnet publish -c Release
docker stop ecommerce
docker rm ecommerce
docker image rm ecommerce
docker build -t ecommerce .
docker run -d -p 8080:80 --name ecommerce ecommerce