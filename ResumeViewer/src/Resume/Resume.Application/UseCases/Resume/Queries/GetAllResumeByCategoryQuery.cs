using MediatR;
using Resume.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resume.Application.UseCases.Resume.Queries
{
    public class GetAllResumeByCategoryQuery : IRequest<List<ResumeModel>>
    {
        public string Status { get; set; } = "";
        public string Exp { get; set; } = "";
        public string[] Skills { get; set; } = [];
        public string Job { get; set; } = "";
        public int PageIndex { get; set; }
        public int Size { get; set; }
    }
}
