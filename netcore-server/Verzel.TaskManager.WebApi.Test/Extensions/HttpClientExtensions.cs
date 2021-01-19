using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using Verzel.TaskManager.WebAPI.Authentication;
using Verzel.TaskManager.WebAPI.Models;

namespace Verzel.TaskManager.WebAPI.Test.Extensions
{
    public static class HttpClientExtensions
    {
        public static HttpClient Authenticated(this HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateToken());
            return client;
        }

        private static string GenerateToken()
        {
            var usuario = new Usuario() { Email = "test@user.com", Nome = "Test User" };
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("2KbsB3ZRFsmw0d4uUpTtACTnpQtf5nZ8f9U7PRltEEUBJ3T4zD8fYHYtFxqO0pBTS208aSmFpCiZaoz0ikKlG1yRkigEHNAbtwiZ");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.Email, usuario.Email)
                }),
                Audience = "https://localhost:443250",
                Issuer = "Verzel.TaskManager",
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
