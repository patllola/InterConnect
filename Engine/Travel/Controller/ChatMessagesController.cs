// Chat/ChatMessagesController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Engine.Models;
using Engine.Data;

namespace Engine.Travel.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatMessagesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChatMessagesController(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("byRequest/{requestId}")]
        public async Task<IActionResult> GetByHelpRequest(Guid requestId)
        {
            var userId = GetUserId();
            var isParticipant = await _context.HelpRequests.AnyAsync(r => r.Id == requestId && (r.SeekerId == userId || r.HelperId == userId));
            if (!isParticipant) return Forbid();

            var messages = await _context.ChatMessages
                .Where(m => m.HelpRequestId == requestId)
                .OrderBy(m => m.SentAt)
                .ToListAsync();
            return Ok(messages);
        }

        [HttpPost]
        public async Task<IActionResult> Send(ChatMessage message)
        {
            var userId = GetUserId();
            var request = await _context.HelpRequests.FindAsync(message.HelpRequestId);
            if (request == null || (request.SeekerId != userId && request.HelperId != userId)) return Forbid();

            message.SenderId = userId.ToString();
            message.SentAt = DateTime.UtcNow;
            _context.ChatMessages.Add(message);
            await _context.SaveChangesAsync();
            return Ok(message);
        }

        private Guid GetUserId()
        {
            return Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
