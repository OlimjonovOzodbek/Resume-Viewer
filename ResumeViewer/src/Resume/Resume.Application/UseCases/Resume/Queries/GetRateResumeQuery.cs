using MediatR;
using Resume.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resume.Application.UseCases.Resume.Queries
{
    public class GetRateResumeQuery : IRequest<ResponseModel>
    {
        public Guid ResumeId { get; set; }
        public string Keywords { get; set; }
    }
}
