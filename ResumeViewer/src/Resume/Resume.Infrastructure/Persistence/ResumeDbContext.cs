using Microsoft.EntityFrameworkCore;
using Resume.Application.Abstractions;
using Resume.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resume.Infrastructure.Persistence
{
    public class ResumeDbContext : DbContext, IResumeDbContext
    {
        public ResumeDbContext(DbContextOptions<ResumeDbContext> options) : base(options) 
        {
            Database.Migrate();
        }

        public DbSet<ResumeModel> Resumes { get; set; }
    }
}
