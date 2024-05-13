using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using User.Application.Abstractions;
using User.Application.UseCases.User.Queries;
using User.Domain.Entities.Models;

namespace User.Application.UseCases.User.Handlers.QueriesHandler
{
    public class GetByIdUsersQueryHandler : IRequestHandler<GetUsersByIdQuery, UserModel>
    {
        private readonly IAppDbContext _context;
        private static SemaphoreSlim _semaphore = new SemaphoreSlim(3);

        public GetByIdUsersQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<UserModel> Handle(GetUsersByIdQuery request, CancellationToken cancellationToken)
        {
            await _semaphore.WaitAsync(cancellationToken);
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                if (user == null)
                    return null;

                return user;
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
