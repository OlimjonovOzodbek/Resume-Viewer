using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.Abstractions;
using User.Application.UseCases.User.Commands;
using User.Domain.Entities.Models;

namespace User.Application.UseCases.User.Handlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ResponceModel>
    {
        private readonly IAppDbContext _context;

        public DeleteUserCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponceModel> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var res = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (res != null)
            {
                _context.Users.Remove(res);

                return new ResponceModel
                {
                    Message = "User Deleted",
                    Status = 200,
                    isSuccess = true,
                };
            }
            return new ResponceModel
            {
                Message = "We couldnt delete",
                Status = 200,
                isSuccess = true
            };
        }
    }
}
