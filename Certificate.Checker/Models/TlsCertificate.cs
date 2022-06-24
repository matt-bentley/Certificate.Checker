using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Certificate.Checker.Models
{
    public class TlsCertificate
    {
        public TlsCertificate(X509Certificate2 cert)
        {
            Issuer = cert.Issuer;
            Subject = cert.Subject;
            ValidFrom = cert.NotBefore;
            ValidTo = cert.NotAfter;
            CommonName = ExtractCommonName(cert.Subject);
            SubjectAlternativeName = ExtractSan(cert.Extensions);
        }

        private static string ExtractCommonName(string subject)
        {
            var commonNameStart = subject.IndexOf("CN=");
            subject = subject.Substring(commonNameStart + "CN=".Length);
            int nextItem = subject.IndexOf(",");
            if (nextItem > -1)
            {
                return subject.Substring(0, nextItem);
            }
            else
            {
                return subject;
            }
        }

        private static List<string> ExtractSan(X509ExtensionCollection certificateExtensions)
        {
            var sans = new List<string>();
            var sanExtension = certificateExtensions.FirstOrDefault(e => e.Oid.FriendlyName == "Subject Alternative Name");
            if (sanExtension != null)
            {
                var asndata = new AsnEncodedData(sanExtension.Oid, sanExtension.RawData);
                var combinedSans = asndata.Format(false);
                sans.AddRange(combinedSans.Replace(" ", "").Replace("DNSName=", "").Split(','));
            }
            return sans;
        }

        public string Issuer { get; private set; }
        public string Subject { get; private set; }
        public string CommonName { get; private set; }
        public DateTime ValidFrom { get; private set; }
        public DateTime ValidTo { get; private set; }
        public List<string> SubjectAlternativeName { get; private set; }
    }
}
