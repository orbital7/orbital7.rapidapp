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
        public static IApplicationBuilder ExtendWebRootStaticFiles(
            this IApplicationBuilder app,
            IWebHostEnvironment env,
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
