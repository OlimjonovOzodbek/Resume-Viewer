using MediatR;
using Resume.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resume.Application.UseCases.Resume.Queries
{
    public class GetAllResumeQuery : IRequest<List<ResumeModel>>
    {
        public int PageIndex { get; set; }
        public int Size { get; set; }
    }
}
