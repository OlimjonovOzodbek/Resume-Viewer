using MediatR;
using Resume.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resume.Application.UseCases.Resume.Queries
{
    public class GetResumeByUserIdQuery : IRequest<ResumeModel>
    {
        public Guid UserId { get; set; }
        public Guid ResumeId { get; set; }
    }
}
