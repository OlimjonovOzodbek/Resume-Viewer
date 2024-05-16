using MediatR;
using Resume.Application.UseCases.Resume.Queries;
using Resume.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resume.Application.UseCases.Resume.Handlers.QueryHandlers
{
    public class GetAllResumeByCategoryQueryHandler : IRequestHandler<GetAllResumeByCategoryQuery, List<ResumeModel>>
    {
        public Task<List<ResumeModel>> Handle(GetAllResumeByCategoryQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
