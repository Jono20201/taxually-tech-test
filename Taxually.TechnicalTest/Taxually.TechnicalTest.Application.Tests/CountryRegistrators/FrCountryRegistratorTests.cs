using System.Text;
using FluentAssertions;
using Taxually.TechnicalTest.Application.Commands;
using Taxually.TechnicalTest.Application.CountryRegistrators;
using Taxually.TechnicalTest.Infrastucture;

namespace Taxually.TechnicalTest.Application.Tests.CountryRegistrators;

public class FrCountryRegistratorTests
{
    
    private FrCountryRegistrator _sut = null!;
    private FakeQueueClient _fakeQueueClient = null!;

    [SetUp]
    public void SetUp()
    {
        _fakeQueueClient = new FakeQueueClient();
        _sut = new FrCountryRegistrator(_fakeQueueClient);
    }

    [Test]
    public async Task WhenRequestPassedIn_ExpectCsvDataPassedToQueueClient()
    {
        await _sut.Register(new RegisterCompanyForVatCommand
        {
            CompanyName = "French Co",
            CompanyId = "5678",
            Country = "FR"
        });

        _fakeQueueClient.LastQueueName.Should().Be("vat-registration-csv");
        _fakeQueueClient.LastString.Should().Be("CompanyName,CompanyId\nFrench Co,5678\n");
    }

    private class FakeQueueClient : IQueueClient
    {
        public string? LastQueueName { get; private set; }
        public string? LastString { get; private set; } 

        public Task EnqueueAsync<TPayload>(string queueName, TPayload payload)
        {
            LastQueueName = queueName;
            
            if(typeof(TPayload) == typeof(byte[]))
            {
                var byteArray = payload as byte[];
                LastString = Encoding.UTF8.GetString(byteArray);
            }
            
            return Task.CompletedTask;
        }
    }
}