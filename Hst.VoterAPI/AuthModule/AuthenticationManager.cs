using Hst.Model;
using Hst.Model.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Hst.VoterAPI.AuthModule
{


    public interface IAuthenticationManager
    {
        string GenerateToken(UserModel user, bool isRefreshToken);
    }

    /// <summary>
    /// AuthManager class is designed to generate the JWT token
    /// </summary>
    public class AuthManager : IAuthenticationManager
    {
        private readonly string _privateKey = string.Empty;
        private readonly int _expireTime = 0;
        protected readonly IConfiguration _configuration = null;
        public AuthManager(IConfiguration configuration)
        {
            _configuration = configuration;
            _privateKey = _configuration.GetSection("appSettings:Secret").Value;// Encoding.UTF8.GetBytes(secret);
            _expireTime = Convert.ToInt32(_configuration.GetSection("appSettings:Expire").Value);
        }

        /// <summary>
        /// Generate JWT Token
        /// </summary>
        /// <param name="user"><UserViewModel User Object/param>
        /// <param name="isRefreshToken">Is it a refresh token</param>
        /// <returns>JWT Token</returns>
        public string GenerateToken(UserModel user, bool isRefreshToken)
        {
            if (!string.IsNullOrEmpty(_privateKey))
            {
                var key = Encoding.ASCII.GetBytes(_privateKey);

                var jwtTokenDescripter = new SecurityTokenDescriptor
                {
                    //Issuer=""  -- Insert issue details 
                    //Audience="" -- Insert Audiance Details
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim("userId", user.Id.ToString()),
                        new Claim("username", (user.FirstName + " " + user.LastName)),
                        new Claim("email", (user.Email))
                    }),
                    Expires = DateTime.Now.AddMinutes(_expireTime * (isRefreshToken ? 2 : 1)),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };

                var jwtTokenHanadler = new JwtSecurityTokenHandler();
                var token = jwtTokenHanadler.CreateToken(jwtTokenDescripter);
                return jwtTokenHanadler.WriteToken(token);
            }
            return string.Empty;
        }

        /// <summary>
        /// Validate the JWT token
        /// </summary>
        /// <param name="authToken">JWT Token String</param>
        /// <returns>JWTSecurityToken object</returns>
        public static JwtSecurityToken GenerateUserClaimFromJWT(string authToken)
        {
            string privateKey = AppSettings.Secret;
            SecurityKey signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(privateKey));
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = signingKey,
                ValidateLifetime = true,
                LifetimeValidator = LifetimeValidator,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken;
            try
            {
                tokenHandler.ValidateToken(authToken, tokenValidationParameters, out validatedToken);
            }
            catch (Exception ex)
            {
                return null;
            }
            return validatedToken as JwtSecurityToken;
        }
        private static bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken token, TokenValidationParameters @params)
        {
            if (expires != null)
            {
                return expires > DateTime.UtcNow;
            }
            return false;
        }
        /// <summary>
        /// Get JWT Token claimes values 
        /// </summary>
        /// <param name="userPayloadToken">JWTSecurityToken Object</param>
        /// <returns>JWTAuthentcationIdentity Object</returns>
        public static JWTAuthenticationIdentity PopulateUserIdentity(JwtSecurityToken userPayloadToken)
        {
            try
            {
                IEnumerable<Claim> claims = ((userPayloadToken)).Claims;
                var userID = Convert.ToInt32(claims.FirstOrDefault(m => m.Type.ToLower() == "userid").Value);
                var userName = Convert.ToString(claims.FirstOrDefault(m => m.Type.ToLower() == "username").Value);
                var email = Convert.ToString(claims.FirstOrDefault(m => m.Type.ToLower() == "email").Value);
                return new JWTAuthenticationIdentity(userID, userName, email);
            }
            catch
            {
                return null;
            }
        }

    }

    public class JWTAuthenticationIdentity : GenericIdentity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
        public JWTAuthenticationIdentity(int userId, string userName, string email)
            : base(userName)
        {
            UserName = userName;
            UserId = userId;
            Email = email;
        }


    }
}
