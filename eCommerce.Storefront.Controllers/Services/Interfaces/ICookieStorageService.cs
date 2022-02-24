using System;

namespace eCommerce.Storefront.Controllers.Services.Interfaces
{
    public interface ICookieStorageService
    {
        void Save(string key, string value, DateTime expires);
        string Retrieve(string key);
    }
}