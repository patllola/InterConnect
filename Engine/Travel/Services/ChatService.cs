namespace Connect.Travel.Services;

public class ChatService
{
    public interface IChatService
    {
        Task<List<ChatMessage>> GetByHelpRequestAsync(Guid requestId, Guid userId);
        Task<ChatMessage> SendMessageAsync(ChatMessage message, Guid userId);
    }

// Services/ChatService.cs
    public class ChatService : IChatService
    {
        private readonly AppDbContext _context;
        public ChatService(AppDbContext context) => _context = context;

        public async Task<List<ChatMessage>> GetByHelpRequestAsync(Guid requestId, Guid userId)
        {
            var isParticipant = await _context.HelpRequests.AnyAsync(r => r.Id == requestId && (r.SeekerId == userId || r.HelperId == userId));
            if (!isParticipant) return new List<ChatMessage>();

            return await _context.ChatMessages
                .Where(m => m.HelpRequestId == requestId)
                .OrderBy(m => m.SentAt)
                .ToListAsync();
        }

        public async Task<ChatMessage> SendMessageAsync(ChatMessage message, Guid userId)
        {
            var request = await _context.HelpRequests.FindAsync(message.HelpRequestId);
            if (request == null || (request.SeekerId != userId && request.HelperId != userId)) return null;

            message.SenderId = userId.ToString();
            message.SentAt = DateTime.UtcNow;
            _context.ChatMessages.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }
    }

}