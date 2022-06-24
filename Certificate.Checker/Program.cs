using Certificate.Checker;
using Certificate.Checker.Interfaces;
using Certificate.Checker.Models;
using Certificate.Checker.Results;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<ICertificateStore, CertificateStore>();
builder.Services.AddSingleton<CertificateExtractionHandler>();
builder.Services.AddHttpClient<ICertificateCheckerService, CertificateCheckerService>()
                .ConfigurePrimaryHttpMessageHandler((serviceProvider) => serviceProvider.GetRequiredService<CertificateExtractionHandler>());

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapPost("/check", async ([FromBody] CheckRequest request, ICertificateCheckerService checker) =>
{
    var response = await checker.CheckAsync(request.Uri);
    return response;
})
.WithName("Check")
.Produces<CheckResponse>(200);

app.MapGet("/validate", async (string requestUri, ICertificateCheckerService checker) =>
{
    var response = await checker.CheckAsync(requestUri);
    return new CheckResult(response);
})
.WithName("Validate")
.Produces<CheckResponse>(200)
.ProducesProblem(502);

app.Run();