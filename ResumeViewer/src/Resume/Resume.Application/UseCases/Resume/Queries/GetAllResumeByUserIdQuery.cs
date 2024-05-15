using MediatR;
using Resume.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resume.Application.UseCases.Resume.Queries
{
    public class GetAllResumeByUserIdQuery : IRequest<List<ResumeModel>>
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public int PageIndex { get; set; }
        public int Size { get; set; }
    }
}
