using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Microsoft.AspNetCore.Builder
{
    public static class StaticFileExtensions
    {
        public static IApplicationBuilder UseLocalRAStaticFiles(
            this IApplicationBuilder app)
        {
            var localWWWRootPath = Path.Combine(Directory.GetCurrentDirectory(), @"bin\debug\net461\wwwroot");
            if (Directory.Exists(localWWWRootPath))
            {
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(localWWWRootPath),
                    RequestPath = ""
                });
            }

            return app;
        }
    }
}
