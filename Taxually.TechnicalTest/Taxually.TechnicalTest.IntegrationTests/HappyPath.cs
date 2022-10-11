using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Taxually.TechnicalTest.Contracts;


namespace Taxually.TechnicalTest.IntegrationTests;

public class HappyPath
{
    private HttpClient _client;

    [SetUp]
    public void Setup()
    {
        // Arrange
        var application = new WebApplicationFactory<Program>();
        _client = application.CreateClient();
    }

    [TestCase("GB")]
    [TestCase("FR")]
    [TestCase("DE")]
    public async Task WhenCallMadeToRegisterGbEntity_ExpectSuccessfulCall(string countryCode)
    {
        var response = await _client.PostAsJsonAsync("/api/vatregistration", new VatRegistrationRequest
        {
            CompanyName = "Abc Ltd",
            CompanyId = "1234",
            Country = countryCode
        });

        response.Should().BeSuccessful();
    }
}