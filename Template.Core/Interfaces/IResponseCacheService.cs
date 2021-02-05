using System;
using System.Threading.Tasks;

namespace Template.Core.Interfaces
{
    public interface IResponseCacheService 
    {
        Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive);
        Task<string> GetCachedResponseAsync(string cacheKey);
    }
}
