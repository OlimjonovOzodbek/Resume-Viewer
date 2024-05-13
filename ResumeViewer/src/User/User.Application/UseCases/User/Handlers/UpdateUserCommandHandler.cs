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
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ResponceModel>
    {
        private readonly IAppDbContext _context;

        public UpdateUserCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponceModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var res = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (res != null)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
                if (user != null)
                {
                    res.Name = request.Name;
                    res.Email = request.Email;
                    res.Password = request.Password;

                    _context.Users.Update(res);
                    await _context.SaveChangesAsync(cancellationToken);

                    return new ResponceModel
                    {
                        Message = "Changes saved!",
                        Status = 200,
                        isSuccess = true
                    };
                }

                return new ResponceModel
                {
                    Message = "Such email already exists",
                    Status = 409,
                };
            }

            return new ResponceModel
            {
                Message = "Not found!",
                Status = 404
            };
        }
    }
}
