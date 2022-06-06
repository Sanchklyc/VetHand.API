using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Results;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class JwtTokenHelper : ITokenHelper
    {
        private DateTime _accessTokenExpiration;
        private IConfiguration _configuration { get; }
        private TokenOptions _tokenOptions;
        private TokenValidationParameters _tokenValidationParameters;
        public JwtTokenHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
            _tokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = _tokenOptions.Issuer,
                ValidAudience = _tokenOptions.Audince,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey)
            };
        }

        public AccessToken CreateToken(User user)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigninCredentialsHelper.CreateSigningCredential(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration,
            };
        }

        public IIdentity ValidateToken(string jwt)
        {
            var validator = new JwtSecurityTokenHandler();
            var principal = validator.ValidateToken(jwt, _tokenValidationParameters, out var validatedToken);
            return principal.Identity;
        }
        
        private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user, Microsoft.IdentityModel.Tokens.SigningCredentials signingCredentials)
        {
            var jwt = new JwtSecurityToken(
                issuer:tokenOptions.Issuer,
                audience:tokenOptions.Audince,
                claims:null,
                notBefore:DateTime.Now,
                expires:_accessTokenExpiration,
                signingCredentials:signingCredentials
                );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddName($"{user.Name} {user.Surname}");
            return claims;
        }
    }
}
