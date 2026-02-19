using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Student_Management_System_V2.DTOs.Auth;
using Student_Management_System_V2.Helpers;
using Student_Management_System_V2.Models.Identity;
using Student_Management_System_V2.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Student_Management_System_V2.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtHelper _jwt;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            IOptions<JwtHelper> jwtOptions
        )
        {
            _userManager = userManager;
            _jwt = jwtOptions.Value; 
        }

        public async Task<AuthModel> LogIn(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user,dto.Password))
                return new AuthModel {Message = "Email or Password is not valid" };

            var jwtToken = await GenerateToken(user);
            var rolesList = await _userManager.GetRolesAsync(user); 

            return new AuthModel
            {
                Email = dto.Email,
                UserName = dto.Username,
                ExporesON = jwtToken.ValidTo,
                IsAuthenticated = true,
                Roles = rolesList.ToList(),
                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                Message = "Welcome again ^_^ "
            };
        }

        public async Task<AuthModel> Register(RegisterDto dto)
        {
            var FoundMail = await _userManager.FindByEmailAsync(dto.Email);
            var FoundByUserName = await _userManager.FindByNameAsync(dto.Username);

            if (FoundByUserName != null || FoundMail != null)
                return new AuthModel {Message = "Email or UserName are already exist" };

            var newAppUser = new ApplicationUser
            {
                Email = dto.Email,
                UserName = dto.Username,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Address = dto.Address
            };

            var result = await _userManager.CreateAsync(newAppUser,dto.Password);

            if(!result.Succeeded)
            {
                var ErrorList = "";
                foreach (var error in result.Errors)
                    ErrorList += error.Description + ' ';
                return new AuthModel { Message = $"There are some errors : {ErrorList}" };
            }
            await _userManager.AddToRoleAsync(newAppUser, "user");

            // Generate Token

            var token = await GenerateToken(newAppUser);
            return new AuthModel
            {
                Email = newAppUser.Email,
                ExporesON = token.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "user" },
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserName = newAppUser.UserName
            };

        }
        private async Task<SecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = roles
                .Select(role => new Claim(ClaimTypes.Role, role))
                .ToList();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName), 
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim("userID" , user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwt.Key)
            );

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_jwt.DurationInDays),
                signingCredentials: creds
            );

            return token;



        }
    }
}
