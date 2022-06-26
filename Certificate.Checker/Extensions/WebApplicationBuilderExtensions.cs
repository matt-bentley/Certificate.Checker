using Certificate.Checker.Filters;

namespace Microsoft.AspNetCore.Mvc
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddPathBase(this WebApplicationBuilder builder)
        {
            var pathBase = builder.Configuration["PATH_BASE"];
            if (!string.IsNullOrEmpty(pathBase))
            {
                builder.Services.AddSingleton<IStartupFilter>(new PathBaseStartupFilter(pathBase));
            }
            return builder;
        }
    }
}
