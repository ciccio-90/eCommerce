# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/aspnet:8.0
COPY eCommerce.Storefront.UI.Web.MVC/bin/Release/net8.0/publish/ eCommerce/
WORKDIR /eCommerce
ENTRYPOINT ["dotnet", "eCommerce.Storefront.UI.Web.MVC.dll"]