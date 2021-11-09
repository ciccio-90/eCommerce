# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/aspnet:6.0
COPY eCommerce.Storefront.UI.Web.MVC/bin/Release/net6.0/publish/ eCommerce/
WORKDIR /eCommerce
ENTRYPOINT ["dotnet", "eCommerce.Storefront.UI.Web.MVC.dll"]