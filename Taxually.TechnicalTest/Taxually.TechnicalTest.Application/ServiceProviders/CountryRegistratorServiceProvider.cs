using Taxually.TechnicalTest.Application.CountryRegistrators;

namespace Taxually.TechnicalTest.Application.ServiceProviders;

public class CountryRegistratorServiceProvider : IServiceProvider<ICountryRegistrator>
{
    private readonly IServiceProvider _serviceProvider;

    private Dictionary<string, Type> _registrators = new()
    {
        { "GB", typeof(GbCountryRegistrator) },
        { "FR", typeof(FrCountryRegistrator) },
        { "DE", typeof(DeCountryRegistrator) }
    };

    public CountryRegistratorServiceProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public bool TryGetService(string key, out ICountryRegistrator? service)
    {
        if (!_registrators.TryGetValue(key, out var type))
        {
            service = null;
            return false;
        }

        service = (ICountryRegistrator?) _serviceProvider.GetService(type);
        return true;
    }
}