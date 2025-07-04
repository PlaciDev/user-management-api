﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UseManagementApi.Models;

namespace UseManagementApi.Services;

public class TokenService
{
    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role.Name)
                
            }),
            
            Expires = DateTime.UtcNow.AddHours(8),
            
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}