using Certificate.Checker.Interfaces;
using Certificate.Checker.Models;
using Certificate.Checker.Results;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.AddPathBase();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCertificateChecker();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapPost("/check", async ([FromBody] CheckRequest request, ICertificateCheckerService checker) =>
{
    if (!IsValidUri(request.Uri))
    {
        return Results.BadRequest("Invalid Uri");
    }
    var response = await checker.CheckAsync(request.Uri);
    return Results.Ok(response);
})
.WithName("Check")
.Produces<CheckResponse>(200)
.ProducesValidationProblem();

app.MapGet("/validate", async (string requestUri, ICertificateCheckerService checker) =>
{
    if (!IsValidUri(requestUri))
    {
        return Results.BadRequest("Invalid Uri");
    }
    var response = await checker.CheckAsync(requestUri);
    return new CheckResult(response);
})
.WithName("Validate")
.Produces<CheckResponse>(200)
.ProducesValidationProblem()
.ProducesProblem(502);

app.Run();

bool IsValidUri(string uri)
{
    try
    {
        _ = new Uri(uri);
        return true;
    }
    catch
    {
        return false;
    }
}