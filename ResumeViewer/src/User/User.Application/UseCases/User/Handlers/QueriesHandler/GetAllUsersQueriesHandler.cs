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

namespace User.Application.UseCases.User.Handlers.QueriesHandler
{
    public class GetAllUsersQueriesHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserModel>>
    {
        private readonly IAppDbContext _context;

        public GetAllUsersQueriesHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<UserModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var res = await _context.Users.ToListAsync(cancellationToken);
            return res;
        }
    }
}
