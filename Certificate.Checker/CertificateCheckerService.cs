using Certificate.Checker.Interfaces;
using Certificate.Checker.Models;

namespace Certificate.Checker
{
    public class CertificateCheckerService : ICertificateCheckerService
    {
        private readonly HttpClient _client;
        private readonly ICertificateStore _certificateStore;
        private ILogger<CertificateCheckerService> _logger;

        public CertificateCheckerService(HttpClient client,
            ICertificateStore certificateStore,
            ILogger<CertificateCheckerService> logger)
        {
            _client = client;
            _certificateStore = certificateStore;
            _logger = logger;
        }

        public async ValueTask<CheckResponse> CheckAsync(string checkUri)
        {
            _logger.LogInformation("Checking for {requestUri}", checkUri);
            var tlsCertificate = _certificateStore.Find(checkUri);
            if(tlsCertificate == null)
            {
                await _client.SendAsync(new HttpRequestMessage(HttpMethod.Head, checkUri));
                tlsCertificate = _certificateStore.Find(checkUri);
            }
            return new CheckResponse(checkUri, tlsCertificate);
        }
    }
}
