using Taxually.TechnicalTest.Application.Commands;

namespace Taxually.TechnicalTest.Application.CountryRegistrators;

public interface ICountryRegistrator
{
    public string CountryCode { get; }
    public Task Register(RegisterCompanyForVatCommand request);
}