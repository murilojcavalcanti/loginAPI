using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using usuarioApi.Models;

namespace usuarioApi.Services;

public class TokenService
{
    private IConfiguration Configuration;

    public TokenService(IConfiguration _Configuration)
    {
        Configuration = _Configuration;
    }
    public string GenerateToken(Usuario usuario)
    {
        
        Claim[] claims = new Claim[]
        {
            new Claim("username", usuario.UserName),
            new Claim("id", usuario.Id),
            new Claim(ClaimTypes.DateOfBirth, usuario.DataNascimento.ToString()),
            new Claim("loginTimestamp",DateTime.UtcNow.ToString())
        };

        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SymmetricSecurityKey"]));

        var signingCredentials = new SigningCredentials(chave,SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken
            (
            expires: DateTime.Now.AddMinutes(10),
            claims: claims,
            signingCredentials: signingCredentials
            );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}