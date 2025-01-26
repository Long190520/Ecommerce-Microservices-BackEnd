using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UserService.Service
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenDto GetUserToken(User user, string? refreshToken = null, bool autoSigin = false)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var (token, exp) = AccessToken(authClaims);

            refreshToken ??= UserUtils.GenerateRandomToken();

            var res = new TokenDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                AccessTokenExp = exp,
                RefreshToken = refreshToken,
                RefreshTokenExp = DateTime.Now.AddDays(1)
            };

            return res;
        }

        private (JwtSecurityToken, DateTime) AccessToken(IEnumerable<Claim> claims)
        {
            var exp = DateTime.Now.AddMinutes(30);
            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]!));
            var creds = new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(_configuration["JWT:ValidIssuer"],
                                             _configuration["JWT:ValidAudience"],
                                             claims,
                                             expires: exp,
                                             signingCredentials: creds);
            return (token, exp);
        }
    }
}
