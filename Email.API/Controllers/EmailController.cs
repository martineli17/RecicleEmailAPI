using Aplicacao.Contratos;
using Aplicacao.DTO;
using Email.API.Core;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Email.API.Controllers
{
    [ApiController, ApiKeyAuthorize]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("email")]
    public class EmailController : Controller
    {
        private readonly IEmailServiceAplicacao _emailService;
        public EmailController(IEmailServiceAplicacao emailService)
        {
            _emailService = emailService;
        }
        [HttpPost]
        public async Task<ActionResult> Enviar([FromBody]EmailDTO email)
        {
            var result = await _emailService.Enviar(email);
            return result ? Ok() : BadRequest();
        }
    }
}
