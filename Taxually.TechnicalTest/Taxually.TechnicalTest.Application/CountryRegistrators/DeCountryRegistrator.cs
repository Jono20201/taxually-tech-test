using System.Xml.Serialization;
using Taxually.TechnicalTest.Application.Commands;
using Taxually.TechnicalTest.Infrastucture;

namespace Taxually.TechnicalTest.Application.CountryRegistrators;

public class DeCountryRegistrator : ICountryRegistrator
{
    private readonly IQueueClient _queueClient;

    public DeCountryRegistrator(IQueueClient queueClient)
    {
        _queueClient = queueClient;
    }

    public string CountryCode => "DE";

    public Task Register(RegisterCompanyForVatCommand request)
    {
        using var stringWriter = new StringWriter();
        
        // todo: Follow SOLID principles and move XML serialization into another class.
        var serializer = new XmlSerializer(typeof(RegisterCompanyForVatCommand));
        serializer.Serialize(stringWriter, request);
        var xml = stringWriter.ToString();
        
        return _queueClient.EnqueueAsync("vat-registration-xml", xml);
    }
}