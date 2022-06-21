using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace BaseApplication.CacheManager
{
    public interface ICacheManager : ITransientDependency
    {
        Task<TCacheItem> GetCacheAsync<TCacheItem>(string group, string key);
        Task<string> GetCacheStringAsync(string group, string key);
        Task<TCacheItem> GetOrAddAsync<TCacheItem>(
            string group,
            string key,
            Func<Task<TCacheItem>> factory,
            DistributedCacheEntryOptions options = null,
            bool? hideErrors = null,
            bool considerUow = false);
        Task SetCacheAsync(
            string group,
            string key,
            object value,
            DistributedCacheEntryOptions options = null,
            bool? hideErrors = null,
            bool considerUow = false);

        Task RemoveCacheAsync(string group, string key);
        Task RemoveAllCache(string group);
    }

}
