using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.DTOs;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagement.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtSettings _jwtSettings;
        private readonly IPasswordHasher<string> _passwordHasher;
        private readonly IRoleRepository _roleRepository;
        public AuthService(IUserRepository userRepository, IOptions<JwtSettings> jwtSettings, IPasswordHasher<string> passwordHasher, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _jwtSettings = jwtSettings.Value;
            _passwordHasher = passwordHasher;
            _roleRepository = roleRepository;
        }

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);

            if (user == null)
            {
                return null; 
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username)
        };

            var roles = await _roleRepository.GetRolesForUserAsync(user.Id);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                Audience= _jwtSettings.Audience,
                Issuer = _jwtSettings.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);


        }
       
        public async Task<bool> RegisterAsync(RegisterModel model)
        {
            if (await _userRepository.GetUserByUsernameAsync(model.Username) != null)
            {
                return false; // Usuario ya registrado
            }

            string hashedPassword = _passwordHasher.HashPassword(model.Username, model.Password);

            var newUser = new User
            {
                Username = model.Username,
                Password = hashedPassword // Guardar la contraseña hasheada
            };

            await _userRepository.AddUserAsync(newUser);



            if (model.Roles != null && model.Roles.Length > 0)
            {
                foreach (var role in model.Roles)
                {
                    await _roleRepository.AssignRoleToUserAsync(newUser.Id, role);
                }
            }

            return true;
        }


    }
}
