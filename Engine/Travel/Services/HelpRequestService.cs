namespace Connect.Travel.Services;

public class HelpRequestService
{
    public interface IHelpRequestService
    {
        Task<List<HelpRequest>> GetAllAsync(Guid userId);
        Task<HelpRequest> CreateAsync(HelpRequest request, Guid seekerId);
        Task<HelpRequest> AcceptAsync(Guid requestId, Guid helperId);
    }

// Services/HelpRequestService.cs
    public class HelpRequestService : IHelpRequestService
    {
        private readonly AppDbContext _context;
        public HelpRequestService(AppDbContext context) => _context = context;

        public async Task<List<HelpRequest>> GetAllAsync(Guid userId) =>
            await _context.HelpRequests.Where(r => r.SeekerId == userId || r.HelperId == userId).ToListAsync();

        public async Task<HelpRequest> CreateAsync(HelpRequest request, Guid seekerId)
        {
            request.SeekerId = seekerId;
            request.Status = "Pending";
            _context.HelpRequests.Add(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<HelpRequest> AcceptAsync(Guid requestId, Guid helperId)
        {
            var request = await _context.HelpRequests.FirstOrDefaultAsync(r => r.Id == requestId && r.HelperId == helperId);
            if (request == null) return null;
            request.Status = "Accepted";
            await _context.SaveChangesAsync();
            return request;
        }
    }
}