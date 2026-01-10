using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using Connect.Travel.Services;
using Shared.Models.HelpRequest.Models;
using Microsoft.AspNetCore.Http;

namespace Engine.Travel.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HelpRequestsController : ControllerBase
    {
        private readonly IHelpRequestService _service;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HelpRequestsController(IHelpRequestService service, IHttpContextAccessor httpContextAccessor)
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userId = GetUserId();
            var details = await _service.GetDetailsAsync(id, userId);
            return details == null ? NotFound() : Ok(details);
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

        [HttpPut("{id}/complete")]
        public async Task<IActionResult> Complete(Guid id)
        {
            var userId = GetUserId();
            var updated = await _service.CompleteAsync(id, userId);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpPost("{id}/pay")]
        public async Task<IActionResult> Pay(Guid id)
        {
            var seekerId = GetUserId();
            var updated = await _service.PayAsync(id, seekerId);
            return updated == null ? BadRequest("Cannot pay for this request.") : Ok(updated);
        }

        private Guid GetUserId()
        {
            return Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}