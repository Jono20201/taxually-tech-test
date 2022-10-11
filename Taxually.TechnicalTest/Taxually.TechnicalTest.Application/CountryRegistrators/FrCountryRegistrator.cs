using System.Text;
using Taxually.TechnicalTest.Application.Commands;
using Taxually.TechnicalTest.Infrastucture;

namespace Taxually.TechnicalTest.Application.CountryRegistrators;

public class FrCountryRegistrator : ICountryRegistrator
{
    private readonly IQueueClient _queueClient;

    public FrCountryRegistrator(IQueueClient queueClient)
    {
        _queueClient = queueClient;
    }
    
    public string CountryCode => "FR";
    
    public Task Register(RegisterCompanyForVatCommand request)
    {
        var csvBuilder = new StringBuilder();
        
        csvBuilder.AppendLine("CompanyName,CompanyId");
        csvBuilder.AppendLine($"{request.CompanyName},{request.CompanyId}");
        
        // todo: Follow SOLID principles and move string -> bytes into own class.
        var csv = Encoding.UTF8.GetBytes(csvBuilder.ToString());

        return _queueClient.EnqueueAsync("vat-registration-csv", csv);
    }
}