using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Microsoft.AspNetCore.Builder
{
    public static class StaticFileExtensions
    {
        //public static IApplicationBuilder UseLocalRAStaticFiles(
        //    this IApplicationBuilder app)
        //{
        //    var localWWWRootPath = Path.Combine(Directory.GetCurrentDirectory(), @"bin\debug\net461\wwwroot");
        //    if (Directory.Exists(localWWWRootPath))
        //    {
        //        app.UseStaticFiles(new StaticFileOptions
        //        {
        //            FileProvider = new PhysicalFileProvider(localWWWRootPath),
        //            RequestPath = ""
        //        });
        //    }

        //    return app;
        //}

        public static IApplicationBuilder ExtendWebRootStaticFiles(
            this IApplicationBuilder app,
            IHostingEnvironment env,
            string physicalPath,
            string requestPath)
        {
            if (Directory.Exists(physicalPath))
            {
                var compositeProvider = new CompositeFileProvider(
                    env.WebRootFileProvider,
                    new PhysicalFileProvider(physicalPath));
                env.WebRootFileProvider = compositeProvider;
                var options = new StaticFileOptions()
                {
                    FileProvider = compositeProvider,
                    RequestPath = requestPath,
                };
                app.UseStaticFiles(options);
            }

            return app;
        }
    }
}
