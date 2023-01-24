using KafkaWithAvro.Domain.Commands;
using KafkaWithAvro.Domain.Commands.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace KafkaWithAvro.Producer.Api.Controllers
{
    [ApiController]
    [Route("v1/users")]
    public class UserController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromServices] CreateUserHandler handler,
            [FromBody] CreateUserCommand command)
        {
            await handler.HandleAsync(command);
            return Ok();
        }
    }
}
