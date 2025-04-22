using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Connect.Travel.Services;
using Engine.Models;
using Engine.Services;

namespace Engine.Travel.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HelpRequestsController : ControllerBase
    {
        private readonly HelpRequestService.IHelpRequestService _service;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HelpRequestsController(HelpRequestService.IHelpRequestService service, IHttpContextAccessor httpContextAccessor)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();
            return Ok(await _service.GetAllAsync(userId));
        }

        [HttpPost]
        public async Task<IActionResult> Create(HelpRequest request)
        {
            var seekerId = GetUserId();
            var result = await _service.CreateAsync(request, seekerId);
            return Ok(result);
        }

        [HttpPut("{id}/accept")]
        public async Task<IActionResult> Accept(Guid id)
        {
            var helperId = GetUserId();
            var updated = await _service.AcceptAsync(id, helperId);
            return updated == null ? NotFound() : Ok(updated);
        }

        private Guid GetUserId()
        {
            return Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}