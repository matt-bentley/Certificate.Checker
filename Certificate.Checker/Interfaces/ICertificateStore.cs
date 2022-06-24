using Certificate.Checker.Models;

namespace Certificate.Checker.Interfaces
{
    public interface ICertificateStore
    {
        TlsCertificate Find(string requestUri);
        void Save(string requestUri, TlsCertificate certificate);
    }
}
