using Taxually.TechnicalTest.Application.Commands;
using Taxually.TechnicalTest.Infrastucture;

namespace Taxually.TechnicalTest.Application.CountryRegistrators;

public class GbCountryRegistrator : ICountryRegistrator
{
    private readonly IHttpClient _httpClient;

    public GbCountryRegistrator(IHttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public string CountryCode => "GB";
    
    public Task Register(RegisterCompanyForVatCommand request)
    { 
        return _httpClient.PostAsync("https://api.uktax.gov.uk", request);
    }
}