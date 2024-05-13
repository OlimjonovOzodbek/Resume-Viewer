using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Application.Abstractions;
using User.Application.Services;
using User.Domain.Entities.DTOs;
using User.Domain.Entities.Models;

namespace User.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ResponceModel> Register(UserDTO user)
        {
            var result = await _service.Register(user);

            return result;
        }

        [HttpPost]
        public async Task<ResponceModel> Login(LoginDTO login)
        {
            var result = await _service.Login(login);

            return result;
        }
    }
}
