using Certificate.Checker.Interfaces;
using Certificate.Checker.Models;

namespace Certificate.Checker
{
    public class CertificateExtractionHandler : HttpClientHandler
    {
        private readonly ICertificateStore _certificateStore;

        public CertificateExtractionHandler(ICertificateStore certificateStore)
        {
            _certificateStore = certificateStore;
            ServerCertificateCustomValidationCallback = (request, cert, chain, policyErrors) =>
            {
                var tlsCertificate = new TlsCertificate(cert);
                _certificateStore.Save(request.RequestUri.ToString(), tlsCertificate);
                return true;
            };
        }
    }
}
