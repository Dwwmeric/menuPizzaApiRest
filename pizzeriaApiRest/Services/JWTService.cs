using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using pizzeriaApiRest.Models;
using pizzeriaApiRest.Respositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace pizzeriaApiRest.Services
{
    public class JWTService 
    {
        private UsersRespository _userRespository;

        public JWTService(UsersRespository userRespository)
        {
            _userRespository = userRespository;
        }

        public string Login(string email, string password)
        {
            Users user = _userRespository.SearchOne(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                //Créer le token 
                JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
                {
                    Expires = DateTime.Now.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Bonjour je suis la clé de sécurité pour générer la JWT")), SecurityAlgorithms.HmacSha256),
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Role, user.Role),
                        new Claim(ClaimTypes.Email, user.Email)
                    }),
                    Issuer = "sogeti",
                    Audience = "sogeti"

                };
                SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
                return jwtSecurityTokenHandler.WriteToken(securityToken);
            }
            return null;
        }
    }
}
