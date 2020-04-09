using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Extensions
{
    public static class IdentityExtensions
    {
        public static string UserId(this ClaimsPrincipal user)
        {
            return user.Claims
                .Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
        }
        
        public static TKey UserId<TKey>(this ClaimsPrincipal user)
        {
            var stringId = user.Claims
                .Single(c => c.Type == ClaimTypes.NameIdentifier).Value;

            // String
            if (typeof(TKey) == typeof(string))
            {
                return (TKey) Convert.ChangeType(stringId, typeof(TKey));
            } 
            // Int, Long or Guid
            else if(typeof(TKey) == typeof(int) || typeof(TKey) == typeof(long))
            {
                if (stringId == null)
                {
                    return (TKey) Convert.ChangeType(0, typeof(TKey));
                }
                return (TKey) Convert.ChangeType(stringId, typeof(TKey));
            }
            else if (typeof(TKey) == typeof(Guid))
            {
                return (TKey) Convert.ChangeType(new Guid(stringId), typeof(TKey));
            }

            else
            {
                throw new Exception("Invalid userId type provided");
            }
        }

        public static Guid UserGuidId(this ClaimsPrincipal user)
        {
            return user.UserId<Guid>();
        }

        /**
         * Generates JWT (Jason web token) to use for the logged in user instead of cookies
         */
        public static string GenerateJWT(IEnumerable<Claim> claims, string signingKey, string issuer, int expiresInDays)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(expiresInDays);

            var token = new JwtSecurityToken(
                issuer,
                issuer,
                claims,
                null,
                expires,
                signingCredentials
            );
            
            return new JwtSecurityTokenHandler().WriteToken(token); 
        }
    }
}