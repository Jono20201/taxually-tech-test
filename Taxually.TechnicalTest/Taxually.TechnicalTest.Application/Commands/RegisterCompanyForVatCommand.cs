using MediatR;

namespace Taxually.TechnicalTest.Application.Commands;

public class RegisterCompanyForVatCommand : IRequest
{
    public string CompanyName { get; set; }
    
    public string CompanyId { get; set; }
    
    public string Country { get; set; }
}