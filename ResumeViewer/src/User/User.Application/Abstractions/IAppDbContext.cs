using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entities.Models;

namespace User.Application.Abstractions
{
    public interface IAppDbContext
    {
        public DbSet<UserModel> Users { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken); 
    }
}
