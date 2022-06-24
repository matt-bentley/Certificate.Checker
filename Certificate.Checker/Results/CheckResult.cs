using Certificate.Checker.Models;
using System.Net.Mime;
using System.Text.Json;

namespace Certificate.Checker.Results
{
    public class CheckResult : IResult
    {
        private readonly CheckResponse _checkResponse;

        public CheckResult(CheckResponse checkResponse)
        {
            _checkResponse = checkResponse;
        }

        public Task ExecuteAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = MediaTypeNames.Application.Json;
            if (_checkResponse.Expired || !_checkResponse.RequestHostMatch)
            {
                httpContext.Response.StatusCode = StatusCodes.Status502BadGateway;
            }
            return JsonSerializer.SerializeAsync(httpContext.Response.Body, _checkResponse, typeof(CheckResponse));
        }
    }
}
