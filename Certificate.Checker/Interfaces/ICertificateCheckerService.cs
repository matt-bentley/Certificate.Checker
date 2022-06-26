using Certificate.Checker.Models;

namespace Certificate.Checker.Interfaces
{
    public interface ICertificateCheckerService
    {
        ValueTask<CheckResponse> CheckAsync(string checkUri);
    }
}
