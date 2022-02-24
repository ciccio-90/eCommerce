using Microsoft.Extensions.Caching.Memory;

namespace eCommerce.Storefront.Services.Cache
{
    public class MemoryCacheAdapter : ICacheStorage
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheAdapter(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void Store(string key, object data)
        {
            _memoryCache.Set(key, data);
        }

        public T Retrieve<T>(string storageKey)
        {
            T itemStored = _memoryCache.Get<T>(storageKey);

            if (itemStored == null)
            {
                itemStored = default(T);
            }

            return itemStored;
        }
    }
}