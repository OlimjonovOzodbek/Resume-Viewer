using MediatR;
using Microsoft.EntityFrameworkCore;
using Resume.Application.Abstractions;
using Resume.Application.UseCases.Resume.Queries;
using Resume.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resume.Application.UseCases.Resume.Handlers.QueryHandlers
{
    public class GetAllResumeQueryHandler : IRequestHandler<GetAllResumeQuery, List<ResumeModel>>
    {
        private readonly IResumeDbContext _context;

        public GetAllResumeQueryHandler(IResumeDbContext context)
        {
            _context = context;
        }

        public async Task<List<ResumeModel>> Handle(GetAllResumeQuery request, CancellationToken cancellationToken)
        {
            var resumes = await _context.Resumes
                .Skip(request.PageIndex - 1)
                .Take(request.Size)
                .ToListAsync(cancellationToken);

            return resumes;
        }
    }
}
