using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Caching;
namespace BaseApplication.CacheManager
{
    public class CacheManager :  ICacheManager
    {
        private readonly IDistributedCache<string> _cacheImplementation;
        private readonly IDistributedCache<List<string>> _cacheGroupKey;

        public CacheManager(IDistributedCache<string> cacheImplementation,
            IDistributedCache<List<string>> cacheGroupKey)
        {
            _cacheImplementation = cacheImplementation;
            _cacheGroupKey = cacheGroupKey;
        }
        public async Task SetCacheAsync(
            string group,
            string key,
            object value,
            DistributedCacheEntryOptions options = null,
            bool? hideErrors = null,
            bool considerUow = false)
        {
            var keys =  GetGroupKey(group) ?? new List<string>();
            var keyInGroup = $"{group}::{key}";
            if (keys?.Any(x => x == keyInGroup) != true)
            {
                keys.Add(keyInGroup);
            }
            SetGroupKey(group, keys);
            var jsonCache = JsonConvert.SerializeObject(value);
            await _cacheImplementation.SetAsync(keyInGroup, jsonCache, options, hideErrors, considerUow);
           
        }

        public async Task<TCacheItem> GetOrAddAsync<TCacheItem>(
            string group,
            string key,
            Func<Task<TCacheItem>> factory,
            DistributedCacheEntryOptions options = null,
            bool? hideErrors = null,
            bool considerUow = false)
        {
            var cacheData = await GetCacheAsync<TCacheItem>(group, key);
            if (cacheData == null || cacheData.Equals(default(TCacheItem)))
            {
                cacheData = await factory();
                await SetCacheAsync(group, key, cacheData, options, hideErrors, considerUow);
            }

            return cacheData;
        }

        public async Task<TCacheItem> GetCacheAsync<TCacheItem>(string group,string key)
        {
            var keyInGroup = $"{group}::{key}";
            var cacheJson = await _cacheImplementation.GetAsync(keyInGroup);
            return string.IsNullOrEmpty(cacheJson) ? default(TCacheItem) : JsonConvert.DeserializeObject<TCacheItem>(cacheJson);
        }
        public async Task<string> GetCacheStringAsync(string group, string key)
        {
            var keyInGroup = $"{group}::{key}";
           return await _cacheImplementation.GetAsync(keyInGroup);

        }

        public  Task RemoveCacheAsync(string group, string key)
        {
            return _cacheImplementation.RemoveAsync($"{group}::{key}");
        }

        public async Task RemoveAllCache(string group)
        {
            var keys =  GetGroupKey(group) ?? new List<string>();
            if (keys?.Any() == true)
            {
                foreach (var key in keys)
                {
                    await _cacheImplementation.RemoveAsync(key);
                }
            }
        }

        private List<string> GetGroupKey(string group)
        {
            return _cacheGroupKey.Get("keyOf" + group);
        }

        private void SetGroupKey(string group, List<string> keyCaches)
        {
             _cacheGroupKey.Set("keyOf" + group, keyCaches, new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
            });
        }
    }
}
