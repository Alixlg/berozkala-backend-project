using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace berozkala_backend.Tools
{
    public class JwtTools
    {
        public static string GenerateJwtToken(string userName, string guid, string? device, DateTime expireDate)
        {
            var claims = new[]
            {
                new Claim("userName", userName),
                new Claim("guid", guid),
                new Claim("device", device ?? "")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("yWCyWsAJwshIf5ZXTw1HTdV9SCyXojkz2L9mvQUqbQy6fzu4WgtDvY15rHcq6EqXxzVFa5qeTs3SVx09VLAWiGaKcxBdBelIIbQKZl4ZIhwkkxmMrUxcIfV32r7dITOqPxfbO14eDFGO7TbdfR8CWjwpsMWqd9SMBXJ46U3DkbPql110LqZZU983IjyxjBhJBY9QAmls2A2WxedziuJlUl5dytRTQxZsGxl1skexi5g22lYOodbJCSi0J383lVK"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "BerozKala.ir",
                audience: "BerozKala.ir",
                claims: claims,
                expires: expireDate,
                signingCredentials: creds);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
    }
}