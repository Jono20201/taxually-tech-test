using MediatR;
using Taxually.TechnicalTest.Application.Commands;
using Taxually.TechnicalTest.Application.CountryRegistrators;
using Taxually.TechnicalTest.Application.ServiceProviders;

namespace Taxually.TechnicalTest.Application.CommandHandler;

public class RegisterCompanyForVatCommandHandler : IRequestHandler<RegisterCompanyForVatCommand, Unit>
{
    private readonly IServiceProvider<ICountryRegistrator> _countryRegistratorServiceProvider;

    public RegisterCompanyForVatCommandHandler(IServiceProvider<ICountryRegistrator> countryRegistratorServiceProvider)
    {
        _countryRegistratorServiceProvider = countryRegistratorServiceProvider;
    }

    public async Task<Unit> Handle(RegisterCompanyForVatCommand request, CancellationToken cancellationToken)
    {
        if (!_countryRegistratorServiceProvider.TryGetService(request.Country, out var service) || service is null)
            throw new Exception("Country not supported");

        await service.Register(request);

        return Unit.Value;
    }
}