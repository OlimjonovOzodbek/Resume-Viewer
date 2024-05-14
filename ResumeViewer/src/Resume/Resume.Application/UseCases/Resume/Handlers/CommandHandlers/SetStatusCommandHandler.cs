using MediatR;
using Microsoft.EntityFrameworkCore;
using Resume.Application.Abstractions;
using Resume.Application.UseCases.Resume.Commands;
using Resume.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resume.Application.UseCases.Resume.Handlers.CommandHandlers
{
    public class SetStatusCommandHandler : IRequestHandler<SetStatusCommand, ResponseModel>
    {
        private readonly IResumeDbContext _context;

        public SetStatusCommandHandler(IResumeDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel> Handle(SetStatusCommand request, CancellationToken cancellationToken)
        {
            var resume = await _context.Resumes.FirstOrDefaultAsync(x => x.Id == request.ResumeId, cancellationToken);

            if (resume != null)
            {
                resume.Status = request.Status;

                _context.Resumes.Update(resume);
                await _context.SaveChangesAsync(cancellationToken);

                return new ResponseModel
                {
                    Message = "Status updated!",
                    Status = 200,
                    isSuccess = true
                };
            }

            return new ResponseModel
            {
                Message = "Resume not found!",
                Status = 404
            };
        }
    }
}
