
namespace Certificate.Checker.Models
{
    public class CheckResponse
    {
        public CheckResponse(string requestUri, TlsCertificate cert)
        {
            RequestUri = requestUri;
            Issuer = cert.Issuer;
            Subject = cert.Subject;
            ValidFrom = cert.ValidFrom;
            ValidTo = cert.ValidTo;
            CommonName = cert.CommonName;
            SubjectAlternativeName = cert.SubjectAlternativeName;
        }

        public string Issuer { get; private set; }
        public string Subject { get; private set; }
        public string CommonName { get; private set; }
        public DateTime ValidFrom { get; private set; }
        public DateTime ValidTo { get; private set; }
        public List<string> SubjectAlternativeName { get; private set; }
        public string RequestUri { get; private set; }

        public bool RequestHostMatch
        {
            get 
            { 
                var uri = new Uri(RequestUri);
                var host = uri.Host;
                return host.Equals(CommonName, StringComparison.OrdinalIgnoreCase) || SubjectAlternativeName.Any(e => host.Equals(e, StringComparison.OrdinalIgnoreCase));
            }
        }

        public bool Expired => DateTime.UtcNow > ValidTo;
    }
}
