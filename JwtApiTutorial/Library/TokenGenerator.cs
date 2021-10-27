using JwtApiTutorial.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtApiTutorial.Library
{
    public class TokenGenerator
    {
        private const double EXPIRE_HOURS = 1.0;

        /// <summary>
        /// Yeni token oluşturur.
        /// </summary>
        /// <returns></returns>
        public static TokenResponse GenerateToken(User user, string secret, string audience, string issuer)
        {
            TokenResponse res = new TokenResponse();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = audience,
                Issuer = issuer,
                Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Username),
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role, user.Role)
                    }),

                //Expire token after some time
                Expires = DateTime.UtcNow.AddHours(EXPIRE_HOURS),

                //Let's also sign token credentials for a security aspect, this is important!!!
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            res.Success = true;
            res.Message = "Token oluşturuldu.";
            res.Value = tokenString;

            return res;
        }
    }
}
