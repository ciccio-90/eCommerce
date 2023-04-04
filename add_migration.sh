#!/bin/bash
dotnet tool update --global dotnet-ef
cd "$(dirname "$0")"
cd eCommerce.Storefront.Repository.EntityFrameworkCore
dotnet ef migrations add $(uuidgen) --startup-project "..\eCommerce.Storefront.UI.Web.MVC\eCommerce.Storefront.UI.Web.MVC.csproj"