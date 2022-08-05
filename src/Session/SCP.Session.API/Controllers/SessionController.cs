using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCP.Common.Models;
using SCP.Session.Application.Services;
using System.Net;

namespace SCP.Session.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = $"cashier, store_manager")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SessionModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetSession(Guid id)
        {
            var result = await _sessionService.GetSession(id);
            return result != null ? Ok(result) : NotFound(null);
        }

        [HttpPost("start")]
        [ProducesResponseType(typeof(SessionModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> StartSession([FromBody] WorkstationDataModel wsModel)
        {
            var result = await _sessionService.StartSession(wsModel);
            return Ok(result);
        }

        [HttpDelete("finish/{id}")]
        [ProducesResponseType(typeof(SessionModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> FinishSession(Guid id)
        {
            var result = await _sessionService.FinishSession(id);
            return result != null ? Ok(result) : NotFound(null);
        }

        [HttpDelete("clear/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ClearSession(Guid id)
        {
            await _sessionService.ClearSession(id);
            return Ok();
        }
    }
}
