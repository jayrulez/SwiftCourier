using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Web.Middlewares
{
    public static class InstallerExtensions
    {
        public static IApplicationBuilder UseInstaller(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<InstallerMiddleware>();
        }
    }
}
