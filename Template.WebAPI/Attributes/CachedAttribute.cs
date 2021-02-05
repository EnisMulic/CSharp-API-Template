using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Core.Interfaces;
using Template.Core.Settings;
using Template.WebAPI.Extensions;

namespace Template.WebAPI.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveSeconds;

        public CachedAttribute(int timeToLiveSeconds)
        {
            _timeToLiveSeconds = timeToLiveSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var cacheSettings = (RedisCacheSettings)context.HttpContext.RequestServices.GetService(typeof(RedisCacheSettings));

            if(!cacheSettings.Enabled)
            {
                await next();
                return;
            }

            var cacheService = (IResponseCacheService)context.HttpContext.RequestServices.GetService(typeof(IResponseCacheService));

            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var cachedResponse = await cacheService.GetCachedResponseAsync(cacheKey);

            if(!string.IsNullOrEmpty(cachedResponse))
            {
                var contentResult = new ContentResult
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result = contentResult;
                return;
            }


            var executedContext = await next();
            
            if(executedContext.Result is OkObjectResult okObjectResult)
            {
                await cacheService.CacheResponseAsync(cacheKey, okObjectResult.Value, TimeSpan.FromSeconds(_timeToLiveSeconds));
            }
        }

        private static string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var id = request.HttpContext.GetUserId();
            var keyBuilder = new StringBuilder();

            keyBuilder.Append($"{request.Path}");

            foreach(var (key, value) in request.Query.OrderBy(i => i.Key))
            {
                keyBuilder.Append($"|{id}-{key}-{value}");
            }

            return keyBuilder.ToString();
        }
    }
}
