using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TODOApp.Api;

public static class AuthUtil {
    public static string CreateToken(User user, string key) {
        var claims = new List<Claim>() {
            new (ClaimTypes.Name, user.Username)
        };

        var k = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var cred = new SigningCredentials(k, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: cred
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}