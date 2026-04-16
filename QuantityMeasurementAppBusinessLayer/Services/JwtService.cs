using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using QuantityMeasurementAppModelLayer.Util;
using QuantityMeasurementAppModelLayer.Entity;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using QuantityMeasurementAppBusinessLayer.Interface;

namespace QuantityMeasurementAppBusinessLayer.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _jwtSettings;

        public JwtService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
               issuer: _jwtSettings.Issuer,
               audience: _jwtSettings.Audience,
               claims: claims,
               expires: DateTime.Now.AddMinutes(30),
               signingCredentials: creds
            );

            string jwtToken= new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }
    }
}
