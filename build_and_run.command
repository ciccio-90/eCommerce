cd "$(dirname "$0")"
dotnet build -c Release
dotnet run -c Release --project eCommerce.Storefront.UI.Web.MVC/eCommerce.Storefront.UI.Web.MVC.csproj