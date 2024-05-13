using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.Abstractions;
using User.Application.Validators;
using User.Domain.Entities.DTOs;
using User.Domain.Entities.Models;

namespace User.Application.Services
{
    public class AuthService : IAuthService
    {
        public IAppDbContext _context { get; set; }

        public AuthService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponceModel> Register(UserDTO userDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == userDTO.Email);
            if (user != null)
            {
                return new ResponceModel
                {
                    Message = "Such email already exists!",
                    Status = 409
                };
            }

            var validator = new RegisterUserValidator();

            var validatorResult = validator.Validate(userDTO);

            if (validatorResult.IsValid)
            {
                user.Name = userDTO.Name;
                user.Email = userDTO.Email;
                user.Password = userDTO.Password;
            
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync(cancellationToken: default);

                return new ResponceModel
                {
                    Message = "You registered",
                    Status = 201,
                    isSuccess = true
                };
            }

            return new ResponceModel
            {
                Message = validatorResult.ToString(),
                Status = 403
            };
        }

        public Task<ResponceModel> Login(LoginDTO loginDTO)
        {
            throw new NotImplementedException();
        }
    }
}
