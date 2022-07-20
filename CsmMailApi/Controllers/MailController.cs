using CsmMail.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CsmMail.Dto;


namespace CsmMailApi.Controllers
{
    [Route("api/mail")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly INotificationService _service;

        public MailController(INotificationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] MailDto dto)
        {
            var result = await _service.SendAsync(dto);
            return Ok(result);
        }
    }
}
