using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Connect.Travel.Services;

public class TravelPlanService
{
    public interface ITravelPlanService
    {
        Task<List<TravelPlan>> GetAllAsync(Guid userId);
        Task<TravelPlan> GetByIdAsync(Guid id, Guid userId);
        Task<TravelPlan> CreateAsync(TravelPlan plan, Guid userId);
        Task<bool> DeleteAsync(Guid id, Guid userId);
    }

// Services/TravelPlanService.cs
    public class TravelPlanService : ITravelPlanService
    {
        private readonly AppDbContext _context;
        public TravelPlanService(AppDbContext context) => _context = context;

        public async Task<List<TravelPlan>> GetAllAsync(Guid userId) =>
            await _context.TravelPlans.Where(p => p.UserId == userId).Include(p => p.User).ToListAsync();

        public async Task<TravelPlan> GetByIdAsync(Guid id, Guid userId) =>
            await _context.TravelPlans.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

        public async Task<TravelPlan> CreateAsync(TravelPlan plan, Guid userId)
        {
            plan.UserId = userId;
            _context.TravelPlans.Add(plan);
            await _context.SaveChangesAsync();
            return plan;
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            var plan = await _context.TravelPlans.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
            if (plan == null) return false;
            _context.TravelPlans.Remove(plan);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}