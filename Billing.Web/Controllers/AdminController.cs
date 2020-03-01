using System.Threading.Tasks;
using Billing.App.Features.QueryModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Billing.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        // GET
        public async Task Index() => await _mediator.Send(new RecreateQueryModel.Command());
    }
}