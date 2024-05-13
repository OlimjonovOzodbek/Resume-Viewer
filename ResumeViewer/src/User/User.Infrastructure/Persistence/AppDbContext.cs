using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using User.Application.Abstractions;
using User.Domain.Entities.Models;

namespace User.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
    }
}