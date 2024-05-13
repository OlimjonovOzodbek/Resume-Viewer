using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.Abstractions;
using User.Application.UseCases.User.Queries;
using User.Domain.Entities.Models;

namespace User.Application.UseCases.User.QueriesHandler
{
    public class GetByIdUsersQueryHandler : IRequestHandler<GetUsersByIdQuery, UserModel>
    {
        private readonly IAppDbContext _context;

        public GetByIdUsersQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<UserModel> Handle(GetUsersByIdQuery request, CancellationToken cancellationToken)
        {
            var res = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (res != null) 
            { 
                return res;
            }
            throw new Exception("Error");
        }
    }
}
