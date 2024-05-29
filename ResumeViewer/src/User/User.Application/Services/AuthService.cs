using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using User.Application.Abstractions;
using User.Application.Validators;
using User.Domain.Entities.DTOs;
using User.Domain.Entities.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Globalization;
using System.Security.Cryptography;

namespace User.Application.Services
{
    public class AuthService : IAuthService
    {
        public IAppDbContext _context { get; set; }
        private readonly IConfiguration _configuration;

        public AuthService(IAppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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
                var userr = new UserModel
                {
                    Name = userDTO.Name,
                    Email = userDTO.Email,
                    Password = HashPassword(userDTO.Password)
                };

                await _context.Users.AddAsync(userr);
                await _context.SaveChangesAsync(cancellationToken: default);

                var login = new LoginDTO
                {
                    Email = userDTO.Email,
                    Password = userDTO.Password
                };

                var result = await Login(login);

                return result;
            }

            return new ResponceModel
            {
                Message = validatorResult.ToString(),
                Status = 403
            };
        }

        public async Task<ResponceModel> Login(LoginDTO loginDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == loginDTO.Email);
            if (user == null)
            {
                return new ResponceModel
                {
                    Message = "Email not found",
                    Status = 404
                };
            }

            if (user.Password != HashPassword(loginDTO.Password))
            {
                return new ResponceModel
                {
                    Message = "Incorrect password",
                    Status = 403
                };
            }

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:secretKey"]!));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            int expirePeriod = int.Parse(_configuration["JWTSettings:ExpireDate"]!);

            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,EpochTime.GetIntDate(DateTime.UtcNow).ToString(CultureInfo.InvariantCulture),ClaimValueTypes.Integer64),

                new Claim("UserName",user.Name!),
                new Claim(ClaimTypes.Role,user.Role!),
                new Claim(ClaimTypes.Email,user.Email!),
            };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["JWTSettings:ValidIssuer"],
                audience: _configuration["JWTSettings:ValidAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirePeriod),
                signingCredentials: credentials);

            string Token = new JwtSecurityTokenHandler().WriteToken(token);

            return new ResponceModel
            {
                Message = Token,
                Status = 200,
                isSuccess = true
            };
        }

        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLowerInvariant();
            }
        }
    }
}
