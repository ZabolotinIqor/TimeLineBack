using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Common;
using Domain.Entities;
using Infrastructure.DataBaseContext;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;


namespace Application.Token
{
    public class TokenService: ITokenService
    {
        private readonly IOptions<JwtConfig> config;
        private readonly TimeLineDbContext context;
        public TokenService(IOptions<JwtConfig> config, TimeLineDbContext context)
        {
            this.config = config;
            this.context = context;
        }
        public LoginResponseDto Execute(Domain.Entities.ApplicationUser user, RefreshToken refreshToken = null)
        {
             var now = DateTime.UtcNow;

                 var claims = new List<Claim>()
                 {
                     new Claim(JwtRegisteredClaimNames.NameId, user.id.ToString()),
                     new Claim(JwtRegisteredClaimNames.Jti, user.id.ToString()),
                     new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUniversalTime().ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                     new Claim(JwtRegisteredClaimNames.Birthdate, user.birthDate),
                 };

                 var expiration = TimeSpan.FromMinutes(Convert.ToInt32(config.Value.JwtExpires));
                 var issuer = config.Value.JwtIssuer;
                 var audience = config.Value.JwtAudience;
                 var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.Value.JwtKey));
                 var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                 if (refreshToken == null)
                 {
                     refreshToken = new RefreshToken()
                     {
                         UserId = user.id,
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
                     userId = user.id,
                     role = user.role,
                     birthDate = user.birthDate
                 };
                 return response;
        }
    }
}