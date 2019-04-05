using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Module3.Middlewares
{
    public class ImageCacheMiddleware
    {
        private const string CacheDirectoryKey = "ImageCacheMiddleware:CacheDirectory";
        private const string CacheCapacity = "ImageCacheMiddleware:CacheCapacity";
        private const string CacheExpirationTimeKey = "ImageCacheMiddleware:CacheExpirationInSeconds";

        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IConfiguration _config;

        private static DateTime _lastCacheRequestDate;
        private static readonly string[] imageSuffixes = new string[] { ".png", ".jpg" };

        private string _requestPath;
        private string _cacheDirectory;
        private int _cacheCapacity;
        private int _cacheExpirationInSeconds;

        private string FileName => _requestPath.Substring(_requestPath.LastIndexOf('/') + 1);

        private string FullFileName => Path.Combine(_cacheDirectory, FileName);

        private bool HasCache => File.Exists(FullFileName);

        public ImageCacheMiddleware(RequestDelegate next,
             ILogger<ImageCacheMiddleware> logger,
             IConfiguration config)
        {
            _next = next;
            _logger = logger;
            _config = config;

            ReadCacheSettings();
            InitializeCacheDirectory();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _requestPath = context.Request.Path;

            // handle next middlware if requested resource is not image
            if (!IsImageRequested())
            {
                await _next(context);
                return;
            }

            await RunCacheFlow(context);
        }

        #region private methods
        private async Task RunCacheFlow(HttpContext context)
        {
            RemoveCacheIfTimeEllapsed();
            UpdateLastCacheRequestTime();
            if (HasCache)
            {
                await RetreiveImageFromCache(context);
            }
            else
            {
                if (!HasCacheCapacityReached())
                {
                    await CacheImage(context);
                    return;
                }

                await _next(context);
            }
        }

        private bool HasCacheCapacityReached()
        {
            var filesCountInCache = Directory.GetFiles(_cacheDirectory).Length;
            if (filesCountInCache >= _cacheCapacity)
            {
                return true;
            }

            return false;
        }

        private async Task RetreiveImageFromCache(HttpContext context)
        {
            var cacheBody = ReadFromCache().ToArray();

            context.Response.Body.Write(cacheBody, 0, cacheBody.Length);
            await _next(context);
        }

        private async Task CacheImage(HttpContext context)
        {
            Stream originalBody = context.Response.Body;
            try
            {
                using (var inputStream = new MemoryStream())
                {
                    context.Response.Body = inputStream;
                    await _next(context);

                    KeepOnDisk(inputStream);
                    _logger.LogDebug($"File '{FullFileName}' cached");
                }
            }
            finally
            {
                context.Response.Body = originalBody;
                await _next(context);
            }
        }

        private void RemoveCacheIfTimeEllapsed()
        {
            if (DateTime.Now.Subtract(_lastCacheRequestDate).TotalSeconds >= _cacheExpirationInSeconds)
            {
                File.Delete(FullFileName);
                _logger.LogDebug($"File '{FullFileName}' removed from cache");
            }
        }

        private static void UpdateLastCacheRequestTime()
        {
            _lastCacheRequestDate = DateTime.Now;
        }

        private MemoryStream ReadFromCache()
        {
            var destination = new MemoryStream();
            using (var source = File.Open(FullFileName, FileMode.Open))
            {
                source.CopyTo(destination);
            }

            return destination;
        }

        private void KeepOnDisk(MemoryStream inputStream)
        {
            using (FileStream outputStream = new FileStream(FullFileName, FileMode.Create))
            {
                inputStream.Seek(0, SeekOrigin.Begin);
                inputStream.CopyTo(outputStream);
            }
        }

        private bool IsImageRequested()
        {
            if (string.IsNullOrEmpty(_requestPath))
            {
                return false;
            }

            return imageSuffixes
                .Any(i => _requestPath.EndsWith(i, StringComparison.InvariantCultureIgnoreCase));
        }

        private void ReadCacheSettings()
        {
            _cacheDirectory = _config.GetValue<string>(CacheDirectoryKey);
            _cacheCapacity = _config.GetValue<int>(CacheCapacity);
            _cacheExpirationInSeconds = _config.GetValue<int>(CacheExpirationTimeKey);
        }
               
        private void InitializeCacheDirectory()
        {
            Directory.CreateDirectory(_cacheDirectory);
        }
        #endregion
    }
}
