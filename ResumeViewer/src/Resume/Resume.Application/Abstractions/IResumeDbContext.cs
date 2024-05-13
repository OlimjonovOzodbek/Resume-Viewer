using Microsoft.EntityFrameworkCore;
using Resume.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resume.Application.Abstractions
{
    public interface IResumeDbContext
    {
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        public DbSet<ResumeModel> Resumes { get; set; }
    }
}
