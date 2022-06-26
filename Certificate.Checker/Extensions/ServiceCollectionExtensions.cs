using Certificate.Checker;
using Certificate.Checker.Interfaces;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCertificateChecker(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICertificateStore, CertificateStore>();
            services.AddSingleton<CertificateExtractionHandler>();
            services.AddHttpClient<ICertificateCheckerService, CertificateCheckerService>()
                            .ConfigurePrimaryHttpMessageHandler((serviceProvider) => serviceProvider.GetRequiredService<CertificateExtractionHandler>());
            return services;
        }
    }
}
