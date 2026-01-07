using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using Connect.Travel.Services;
using Engine.Models;
using Engine.Services;
using Microsoft.AspNetCore.Http;


using Engine.Models.DTOs;

namespace Engine.Travel.Controller;


    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TravelPlansController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TravelPlansController(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var plans = await _context.TravelPlans
                .Include(p => p.User)
                .Select(p => new TravelPlanDto
                {
                    Id = p.Id,
                    UserId = p.UserId,
                    FromCountry = p.FromCountry,
                    ToCountry = p.ToCountry,
                    TravelDate = p.TravelDate,
                    Description = p.Description,
                    User = new UserPublicDto
                    {
                        Id = p.User.Id,
                        Name = p.User.Name
                    }
                })
                .ToListAsync();
            return Ok(plans);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string from, string to, DateTime date)
        {
            var plans = await _context.TravelPlans
                .Where(p => p.FromCountry == from && p.ToCountry == to && p.TravelDate.Date == date.Date)
                .Include(p => p.User)
                .Select(p => new TravelPlanDto
                {
                    Id = p.Id,
                    UserId = p.UserId,
                    FromCountry = p.FromCountry,
                    ToCountry = p.ToCountry,
                    TravelDate = p.TravelDate,
                    Description = p.Description,
                    User = new UserPublicDto
                    {
                        Id = p.User.Id,
                        Name = p.User.Name
                    }
                })
                .ToListAsync();
            return Ok(plans);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TravelPlan plan)
        {
            plan.UserId = GetUserId();
            _context.TravelPlans.Add(plan);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = plan.Id }, plan);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userId = GetUserId();
            var plan = await _context.TravelPlans.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
            return plan == null ? NotFound() : Ok(plan);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = GetUserId();
            var plan = await _context.TravelPlans.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
            if (plan == null) return NotFound();
            _context.TravelPlans.Remove(plan);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private Guid GetUserId()
        {
            return Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
