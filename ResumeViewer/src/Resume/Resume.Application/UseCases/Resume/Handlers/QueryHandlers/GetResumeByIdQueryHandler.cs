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
    public class GetResumeByIdQueryHandler : IRequestHandler<GetResumeByIdQuery, ResumeModel>
    {
        private readonly IResumeDbContext _context;

        public GetResumeByIdQueryHandler(IResumeDbContext context)
        {
            _context = context;
        }

        public async Task<ResumeModel> Handle(GetResumeByIdQuery request, CancellationToken cancellationToken)
        {
            var resume = await _context.Resumes.FirstOrDefaultAsync(x => x.Id == request.ResumeId, cancellationToken);

            return resume;
        }
    }
}
