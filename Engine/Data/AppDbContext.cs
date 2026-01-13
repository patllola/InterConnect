using System;
using Microsoft.EntityFrameworkCore;
using Shared.Models.User.Models;
using Shared.Models.TravelPlan.Model;
using Shared.Models.HelpRequest.Models;
using Shared.Models.ChatMessages.Models;

namespace Engine.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<TravelPlan> TravelPlans { get; set; } = default!;
        public DbSet<HelpRequest> HelpRequests { get; set; } = default!;
        public DbSet<ChatMessage> ChatMessages { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HelpRequest>()
                .HasOne(h => h.Seeker)
                .WithMany()
                .HasForeignKey(h => h.SeekerId);

            modelBuilder.Entity<HelpRequest>()
                .HasOne(h => h.Helper)
                .WithMany()
                .HasForeignKey(h => h.HelperId);
        }
    }
}
