using MediatR;
using Microsoft.AspNetCore.Mvc;
using Resume.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resume.Application.UseCases.Resume.Queries
{
    public class GetResumeByIdQuery : IRequest<IActionResult>
    {
        public Guid ResumeId { get; set; }
    }
}
