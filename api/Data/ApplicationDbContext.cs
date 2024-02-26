using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {
        }
        public DbSet<Journey> Journeys { get; set; }
        public DbSet<AppUserJourney> AppUserJourneys { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppUserJourney>(x=>x.HasKey(a=> new{a.AppUserId,a.JourneyId}));
            // builder.Entity<AppUserJourney>(x =>x.HasKey(a => a.Id));

            builder.Entity<AppUserJourney>().HasOne(u=>u.AppUser).WithMany(u=>u.AppUserJourneys).HasForeignKey(a=>a.AppUserId);
            builder.Entity<AppUserJourney>().HasOne(u=>u.Journey).WithMany(u=>u.AppUserJourneys).HasForeignKey(a=>a.JourneyId);

            base.OnModelCreating(builder);
            List<IdentityRole> roles = new List<IdentityRole>{
                new IdentityRole{
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole{
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);

        }




    }
}