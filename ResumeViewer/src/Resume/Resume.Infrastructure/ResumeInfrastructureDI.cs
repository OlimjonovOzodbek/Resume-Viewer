using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Resume.Application.Abstractions;
using Resume.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resume.Infrastructure
{
    public static class ResumeInfrastructureDI
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IResumeDbContext, ResumeDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("ResumePortal"));
            });

            return services;
        }
    }
}
