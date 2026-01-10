using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using Connect.Travel.Services;
using Engine.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Models.TravelPlan.Dtos.GetDto;
using Shared.Models.TravelPlan.Dtos.CreateDto;
using Shared.Models.User.Dtos.GetDto;
using Shared.Models.TravelPlan.Model;

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
        [AllowAnonymous]
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
        public async Task<IActionResult> Create(CreateTravelPlanDto dto)
        {
            var plan = new TravelPlan
            {
                UserId = GetUserId(),
                FromCountry = dto.FromCountry,
                ToCountry = dto.ToCountry,
                TravelDate = dto.TravelDate,
                Description = dto.Description
            };
            
            _context.TravelPlans.Add(plan);
            await _context.SaveChangesAsync();

            var resultDto = new TravelPlanDto
            {
                Id = plan.Id,
                UserId = plan.UserId,
                FromCountry = plan.FromCountry,
                ToCountry = plan.ToCountry,
                TravelDate = plan.TravelDate,
                Description = plan.Description
            };

            return CreatedAtAction(nameof(GetById), new { id = plan.Id }, resultDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userId = GetUserId();
            var plan = await _context.TravelPlans
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
            
            if (plan == null) return NotFound();

            var dto = new TravelPlanDto
            {
                Id = plan.Id,
                UserId = plan.UserId,
                FromCountry = plan.FromCountry,
                ToCountry = plan.ToCountry,
                TravelDate = plan.TravelDate,
                Description = plan.Description,
                User = new UserPublicDto
                {
                    Id = plan.User.Id,
                    Name = plan.User.Name
                }
            };

            return Ok(dto);
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
