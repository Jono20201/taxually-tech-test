using FluentAssertions;
using Moq;
using Taxually.TechnicalTest.Application.CommandHandler;
using Taxually.TechnicalTest.Application.Commands;
using Taxually.TechnicalTest.Application.CountryRegistrators;
using Taxually.TechnicalTest.Application.ServiceProviders;

namespace Taxually.TechnicalTest.Application.Tests.CommandHandler;

public class RegisterCompanyForVatCommandHandlerTests
{
    private Mock<IServiceProvider<ICountryRegistrator>> _serviceProviderMock = null!;
    private RegisterCompanyForVatCommandHandler _sut = null!;

    [SetUp]
    public void Setup()
    {
        _serviceProviderMock = new Mock<IServiceProvider<ICountryRegistrator>>();
        _sut = new RegisterCompanyForVatCommandHandler(_serviceProviderMock.Object);
    }

    [Test]
    public async Task GivenGbConfiguredServiceProvider_WhenGbRequestedCommand_ThenRegistrarCalledWithCommand()
    {
        var mockCountryRegistrator = new Mock<ICountryRegistrator>();
        var mockedCountryRegistratorObject = mockCountryRegistrator.Object;
        _serviceProviderMock.Setup(s => s.TryGetService(It.Is<string>(e => e == "GB"), out mockedCountryRegistratorObject))
            .Returns(true);

        var command = new RegisterCompanyForVatCommand
        {
            CompanyName = "Test Company Ltd",
            CompanyId = "1234",
            Country = "GB"
        };

        await _sut.Handle(command, default);

        mockCountryRegistrator.Verify(v=>v.Register(command), Times.Once);
    }
    
    [Test]
    public async Task GivenUnconfiguredServiceProvider_WhenGbRequestedCommand_ThenExceptionIsThrown()
    {
        ICountryRegistrator? outVar = null;
        _serviceProviderMock.Setup(s => s.TryGetService(It.Is<string>(e => e == "GB"), out outVar))
            .Returns(false);

        Func<Task> action = async () => await _sut.Handle(new RegisterCompanyForVatCommand
        {
            CompanyName = "Test Company Ltd",
            CompanyId = "1234",
            Country = "GB"
        }, default);

        await action.Should().ThrowAsync<Exception>();
    }
    
    private class TestCountryRegistrator : ICountryRegistrator
    {
        public string CountryCode => "GB";
        
        public Task Register(RegisterCompanyForVatCommand request)
        {
            return Task.CompletedTask;
        }
    }
}