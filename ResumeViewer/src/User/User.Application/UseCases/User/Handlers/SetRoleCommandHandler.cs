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
    public class SetRoleCommandHandler : IRequestHandler<SetRoleCommand, ResponceModel>
    {
        private readonly IAppDbContext _context;

        public SetRoleCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponceModel> Handle(SetRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (user == null)
            {
                return new ResponceModel
                {
                    Message = "User not found!",
                    Status = 404
                };
            }

            user.Role = request.Role;
            _context.Users.Update(user);

            await _context.SaveChangesAsync(cancellationToken);

            return new ResponceModel { Message = "Role updated", Status = 200 ,isSuccess = true };
        }
    }
}
