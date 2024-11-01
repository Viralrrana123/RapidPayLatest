using Microsoft.IdentityModel.Tokens;
using RapidPay.Data.Model;
using RapidPay.Data.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RapidPay.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        IUserRepository userRepository;
        private readonly string secretKey;
        private readonly string issuer;
        private readonly string audience;
        private readonly int expiryInMinutes;
        public AuthenticationService(IUserRepository userRepository, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            secretKey = configuration["Jwt:SecretKey"];
            issuer = configuration["Jwt:Issuer"];
            audience = configuration["Jwt:Audience"];
            expiryInMinutes = int.Parse(configuration["Jwt:ExpiryInMinutes"]);
        }
        public string GenerateToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }),
                Expires = DateTime.UtcNow.AddMinutes(expiryInMinutes),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }

        public async Task<AuthTicket>  LoginAsync(string username, string password)
        {
            AuthTicket ticket = new AuthTicket();
            User user = await userRepository.GetUserByUserNameAsync(username);
            if (user != null && user.Password.Equals(password))
            {
                ticket.Authenticated = true;
                ticket.Token = GenerateToken(username);
            }
            return ticket;
        }
    }
}
