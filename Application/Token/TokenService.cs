using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Common;
using Domain.Entities;
using Infrastructure.DataBaseContext;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;


namespace Application.Token
{
    public class TokenService: ITokenService
    {
        private readonly IConfiguration config;
        private readonly TimeLineDbContext context;
        public TokenService(IConfiguration config, TimeLineDbContext context)
        {
            this.config = config;
            this.context = context;
        }
        public LoginResponseDto Execute(Domain.Entities.ApplicationUser user, RefreshToken refreshToken = null)
        {
             var now = DateTime.UtcNow;

                 var claims = new List<Claim>()
                 {
                     new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                     new Claim(JwtRegisteredClaimNames.Jti, user.Id),
                     new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUniversalTime().ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                     new Claim(JwtRegisteredClaimNames.PhoneNumber, user.PhoneNumber),
                 };

                 var expiration = TimeSpan.FromMinutes(Convert.ToInt32(config["JwtExpires"]));
                 var issuer = config["JwtIssuer"];
                 var audience = config["JwtAudience"];
                 var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["JwtKey"]));
                 var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                 if (refreshToken == null)
                 {
                     refreshToken = new RefreshToken()
                     {
                         UserId = user.Id,
                         Token = Guid.NewGuid().ToString("N"),
                     };
                     context.InsertNew(refreshToken);
                 }

                 refreshToken.IssuedUtc = now;
                 refreshToken.ExpiresUtc = now.Add(expiration);
                 context.SaveChanges();

                 var jwt = new JwtSecurityToken(
                     issuer: issuer,
                     audience: audience,
                     claims: claims.ToArray(),
                     notBefore: now,
                     expires: now.Add(expiration),
                     signingCredentials: credentials);
                 var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                 var response = new LoginResponseDto
                 {
                     accessToken = encodedJwt,
                     refreshToken = refreshToken.Token,
                     expiresIn = (int)expiration.TotalSeconds,
                     userId = user.Id,
                 };
                 return response;
        }
    }
}