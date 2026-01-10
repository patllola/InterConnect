using Microsoft.EntityFrameworkCore;
using Engine.Data;
using Shared.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Connect.Travel.Services;

public interface IHelpRequestService
{
    Task<List<HelpRequest>> GetAllAsync(Guid userId);
    Task<HelpRequest> CreateAsync(HelpRequest request, Guid seekerId);
    Task<HelpRequest> AcceptAsync(Guid requestId, Guid helperId);
    Task<HelpRequest> CompleteAsync(Guid requestId, Guid userId);
    Task<HelpRequest> PayAsync(Guid requestId, Guid seekerId);
    Task<object> GetDetailsAsync(Guid requestId, Guid userId);
}

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
            var request = await _context.HelpRequests.FirstOrDefaultAsync(r => r.Id == requestId);
            if (request == null) return null;
            request.HelperId = helperId;
            request.Status = "Accepted";
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<HelpRequest> CompleteAsync(Guid requestId, Guid userId)
        {
            var request = await _context.HelpRequests.FirstOrDefaultAsync(r => r.Id == requestId && (r.SeekerId == userId || r.HelperId == userId));
            if (request == null) return null;
            request.Status = "Completed";
            request.CompletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<HelpRequest> PayAsync(Guid requestId, Guid seekerId)
        {
            var request = await _context.HelpRequests.FirstOrDefaultAsync(r => r.Id == requestId && r.SeekerId == seekerId);
            if (request == null || request.Status != "Completed") return null;
            request.IsPaid = true;
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<object> GetDetailsAsync(Guid requestId, Guid userId)
        {
            var request = await _context.HelpRequests
                .Include(r => r.Seeker)
                .Include(r => r.Helper)
                .FirstOrDefaultAsync(r => r.Id == requestId && (r.SeekerId == userId || r.HelperId == userId));

            if (request == null) return null;

            bool showFullInfo = request.Status == "Accepted" || request.Status == "Completed";

            return new
            {
                request.Id,
                request.Description,
                request.Status,
                request.Price,
                request.IsPaid,
                Seeker = showFullInfo ? (object)new { request.Seeker.Id, request.Seeker.Name, request.Seeker.Email, request.Seeker.PhoneNumber } 
                                     : new { request.Seeker.Id, request.Seeker.Name },
                Helper = request.Helper == null ? null : (showFullInfo ? (object)new { request.Helper.Id, request.Helper.Name, request.Helper.Email, request.Helper.PhoneNumber }
                                                                     : new { request.Helper.Id, request.Helper.Name })
            };
        }
    }