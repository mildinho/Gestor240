using Dominio.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace API.Biblioteca.JWT
{
    //TODO: TERMINAR LOGINS COM JWT

    //--https://www.youtube.com/watch?v=vAUXU0YIWlU&t=10s
    public static class TokenService
    {
        public static TokenUsuarioDTO Generate(LoginDTO loginDTO)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(Settings.SecretKey);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience= Settings.Audience,    
                Issuer= Settings.Issuer,
                Expires = Settings.Expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                algorithm: SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            //return new TokenUsuario tokenHandler.WriteToken(token);
            return new TokenUsuarioDTO
            {
                Email = loginDTO.Email,
                Token = tokenHandler.WriteToken(token),
                Validade = Settings.Expires
            };

        }
    }
}
