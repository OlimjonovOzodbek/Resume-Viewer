using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using User.Application.Abstractions;
using User.Application.Services;
using User.Domain.Entities.Models;

namespace User.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserModel>().HasData(new UserModel()
            {
                Id = Guid.Parse("12345678-1234-1234-1234-1234567890ab"),
                Name = "SuperAdmin",
                Email = "SuperAdmin@admin.com",
                Password = AuthService.HashPassword("SuperAdmin1"),
                Role = "SuperAdmin"
            });
        }

    }
}
