using Certificate.Checker.Models;

namespace Certificate.Checker.Interfaces
{
    public interface ICertificateCheckerService
    {
        Task<CheckResponse> CheckAsync(string checkUri);
    }
}
