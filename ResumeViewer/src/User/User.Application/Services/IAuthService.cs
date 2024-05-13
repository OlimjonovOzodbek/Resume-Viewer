using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entities.DTOs;
using User.Domain.Entities.Models;

namespace User.Application.Services
{
    public interface IAuthService
    {
        public Task<ResponceModel> Register(UserDTO userDTO);
        public Task<ResponceModel> Login(LoginDTO loginDTO);
    }
}
