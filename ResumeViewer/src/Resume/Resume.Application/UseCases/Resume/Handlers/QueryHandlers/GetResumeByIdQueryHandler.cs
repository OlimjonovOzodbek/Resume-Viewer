using MediatR;
using Microsoft.AspNetCore.Mvc;
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
    public class GetResumeByIdQueryHandler : IRequestHandler<GetResumeByIdQuery, IActionResult>
    {
        private readonly IResumeDbContext _context;

        public GetResumeByIdQueryHandler(IResumeDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Handle(GetResumeByIdQuery request, CancellationToken cancellationToken)
        {
            var resume = await _context.Resumes.FirstOrDefaultAsync(x => x.Id == request.ResumeId, cancellationToken);

            if (resume != null)
            {
                if (System.IO.File.Exists(resume.Document))
                {
                    var memory = new MemoryStream();
                    using (var stream = new FileStream(resume.Document, FileMode.Open))
                    {
                        await stream.CopyToAsync(memory, cancellationToken);
                    }
                    memory.Position = 0;

                    return new FileStreamResult(memory, "application/pdf")
                    {
                        FileDownloadName = $"{resume.Name}.pdf"
                    };
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            else
            {
                return new NotFoundResult();
            }
        }

    }
}
