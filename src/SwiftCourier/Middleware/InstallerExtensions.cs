using Microsoft.AspNet.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Middleware
{
    public static class InstallerExtensions
    {
        public static IApplicationBuilder UseInstaller(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<InstallerMiddleware>();
        }
    }
}
