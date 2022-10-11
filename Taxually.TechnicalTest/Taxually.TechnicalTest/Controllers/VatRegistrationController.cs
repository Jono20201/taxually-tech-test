using System.Text;
using System.Xml.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Taxually.TechnicalTest.Application.Commands;
using Taxually.TechnicalTest.Contracts;
using Taxually.TechnicalTest.Infrastucture;

namespace Taxually.TechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatRegistrationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VatRegistrationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        /// <summary>
        /// Registers a company for a VAT number in a given country
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] VatRegistrationRequest request)
        {
            // todo: add validation, auth?, error/exception handling
            await _mediator.Send(new RegisterCompanyForVatCommand
            {
                CompanyName = request.CompanyName,
                CompanyId = request.CompanyId,
                Country = request.Country
            });

            return Ok();
        }
    }
}
