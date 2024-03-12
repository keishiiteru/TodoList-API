using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TodoList.Contracts.DTOs;
using TodoList.Domain.Entities;
using TodoList.Domain.Services;
using TodoList.Domain.UnitOfWork;

namespace TodoList.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITodoManager _manager;
        private readonly IConfiguration _configuration;

        public AuthService(ITodoManager manager, IConfiguration configuration)
        {
            _manager = manager;
            _configuration = configuration;
        }
        public async Task<User> Register(RegisterDto request)
        {
            try
            {
                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

                User user = new User
                {
                    Username = request.Username,
                    Email = request.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };

                _manager.UserRepository.Insert(user);
                _manager.Save();

                return user;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> Login(LoginDto request)
        {
            try
            {
                var user = _manager.UserRepository
                               .GetQueryable(x => !x.IsDeleted && x.Username == request.Username)
                               .FirstOrDefault();
                if (user == null)
                {
                    return "User not found.";
                }

                if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return "Wrong password.";
                }

                string token = CreateToken(user);

                return token;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            try
            {
                using (var hmac = new HMACSHA512())
                {
                    passwordSalt = hmac.Key;
                    passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            try
            {
                using (var hmac = new HMACSHA512(passwordSalt))
                {
                    var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                    return computeHash.SequenceEqual(passwordHash);
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private string CreateToken(User user)
        {
            try
            {
                List<Claim> claims = new List<Claim>
            {
                new Claim("Username", user.Username),
                new Claim("UserId", user.UserId.ToString()),
                new Claim(ClaimTypes.Role, "User"),
            };

                var tokenKey = _configuration.GetSection("AppSettings:Token").Value;
                // Pad the key with zeros to ensure it's at least 512 bits
                var paddedKey = tokenKey.PadRight(64, '0');

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(paddedKey));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var token = new JwtSecurityToken(
                        claims: claims,
                        expires: DateTime.Now.AddDays(1),
                        signingCredentials: creds
                        );

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                return jwt;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
