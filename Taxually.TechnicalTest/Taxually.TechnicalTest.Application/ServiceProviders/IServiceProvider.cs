using Taxually.TechnicalTest.Application.CountryRegistrators;

namespace Taxually.TechnicalTest.Application.ServiceProviders;

public interface IServiceProvider<T>
{
    public bool TryGetService(string key, out ICountryRegistrator? countryRegistrator);
}