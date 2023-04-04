#!/bin/bash
dotnet tool update --global dotnet-ef
cd "$(dirname "$0")"
cd eCommerce.Storefront.UI.Web.MVC
dotnet ef database update