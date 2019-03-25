using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace Module2.Middlewares
{
    public class ImageCacheMiddleware
    {
        private readonly RequestDelegate _next;

        public ImageCacheMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IConfiguration config)
        {
            Stream originalBody = context.Response.Body;
            try
            {
                using (var memStream = new MemoryStream())
                {
                    context.Response.Body = memStream;
                    await _next(context);

                    if (context.Response.ContentType == "image/jpeg")
                    {
                        ReadCacheSettings(config,
                            out string cacheDirectory,
                            out int maxCachedImageCount,
                            out int cacheExpirationTime);

                        using (Stream outputStream = File.Create(cacheDirectory))
                        {
                            CopyStream(originalBody, outputStream);
                        }
                    }
                }
            }
            finally
            {
                context.Response.Body = originalBody;
            }
        }

        private static void ReadCacheSettings(IConfiguration config, out string cacheDirectory, out int maxCachedImageCount, out int cacheExpirationTime)
        {
            cacheDirectory = config.GetValue<string>("ImageCacheMiddlewareSettings:CacheDirectory");
            maxCachedImageCount = config.GetValue<int>("ImageCacheMiddlewareSettings:MaxCachedImageCount");
            cacheExpirationTime = config.GetValue<int>("ImageCacheMiddlewareSettings:CacheExpirationTime");
        }

        private static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }
    }
}
