using Certificate.Checker.Interfaces;
using Certificate.Checker.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Certificate.Checker
{
    public class CertificateStore : ICertificateStore
    {
        private readonly IMemoryCache _cache;
        private const int EXPIRY_MINUTES = 60;

        public CertificateStore(IMemoryCache cache)
        {
            _cache = cache;
        }

        public TlsCertificate Find(string requestUri)
        {
            return _cache.Get<TlsCertificate>(GetRequestHost(requestUri));
        }

        public void Save(string requestUri, TlsCertificate certificate)
        {
            _cache.Set(GetRequestHost(requestUri), certificate, TimeSpan.FromMinutes(EXPIRY_MINUTES));
        }

        private string GetRequestHost(string requestUri)
        {
            return new Uri(requestUri).Host;
        }
    }
}
