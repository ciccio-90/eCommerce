dotnet tool update --global dotnet-ef
cd "$(dirname "$0")"
cd eCommerce.Storefront.Repository.EntityFrameworkCore
dotnet ef migrations remove