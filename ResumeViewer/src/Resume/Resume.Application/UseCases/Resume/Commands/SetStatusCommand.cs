using iText.Kernel.Pdf.Tagging;
using MediatR;
using Resume.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resume.Application.UseCases.Resume.Commands
{
    public class SetStatusCommand : IRequest<ResponseModel>
    {
        public Guid ResumeId { get; set; }
        public int Score { get; set; }
        public string Status { get; set; }
        public string Token { get; set; }
    }
}
