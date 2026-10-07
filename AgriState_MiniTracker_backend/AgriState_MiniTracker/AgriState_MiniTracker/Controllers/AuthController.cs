using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AgriState_MiniTracker.Data;
using AgriState_MiniTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AgriState_MiniTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _config;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthController(ApplicationDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
            _passwordHasher = new PasswordHasher<User>();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
            {
                return BadRequest(new { message = "Username and password are required." });
            }

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == model.Username.Trim().ToLower());
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid username or password." });
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized(new { message = "Invalid username or password." });
            }

            var token = GenerateJwtToken(user, out DateTime expiresAt);

            return Ok(new AuthResponseDto
            {
                Token = token,
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                ExpiresAt = expiresAt
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
            {
                return BadRequest(new { message = "Username and password are required." });
            }

            var existingUser = await _db.Users.AnyAsync(u => u.Username.ToLower() == model.Username.Trim().ToLower());
            if (existingUser)
            {
                return BadRequest(new { message = "Username is already taken." });
            }

            var user = new User
            {
                Username = model.Username.Trim(),
                Email = string.IsNullOrWhiteSpace(model.Email) ? $"{model.Username.Trim()}@agristate.com" : model.Email.Trim(),
                Role = string.IsNullOrWhiteSpace(model.Role) ? "User" : model.Role.Trim(),
                CreatedAt = DateTime.UtcNow
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            var token = GenerateJwtToken(user, out DateTime expiresAt);

            return Ok(new AuthResponseDto
            {
                Token = token,
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                ExpiresAt = expiresAt
            });
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized();
            }

            var user = await _db.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            });
        }

        private string GenerateJwtToken(User user, out DateTime expiresAt)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var keyString = jwtSettings["Key"] ?? "YourSuperSecretKeyThatIsAtLeast32BytesLong!";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            int expireMinutes = int.TryParse(jwtSettings["ExpireMinutes"], out var mins) ? mins : 60;
            expiresAt = DateTime.UtcNow.AddMinutes(expireMinutes);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiresAt,
                Issuer = jwtSettings["Issuer"] ?? "AgriStateApi",
                Audience = jwtSettings["Audience"] ?? "AgriStateUsers",
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}