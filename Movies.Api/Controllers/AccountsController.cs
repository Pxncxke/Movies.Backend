using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Movies.Api.Exceptions;
using Movies.Api.Models;
using Movies.Api.Models.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Movies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountsController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, 
            SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._signInManager = signInManager;
            this._configuration = configuration;
        }

        [HttpGet]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Get()
        {
           var pagination = await PagedList<UserDto>.CreateAsync(_userManager.Users.Select(u => new UserDto { Id = u.Id, Email = u.Email }), 1, 10);
            return Ok(pagination);
        }

        [HttpPost("makeAdmin")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> MakeAdmin([FromBody] string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException(nameof(UserDto), userId);
            }
            await _userManager.AddToRoleAsync(user, "admin");
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "admin"));
            return Ok();
        }

        [HttpPost("removeAdmin")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> RemoveAdmin([FromBody] string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException(nameof(UserDto), userId);
            }
            await _userManager.RemoveFromRoleAsync(user, "admin");
            await _userManager.RemoveClaimAsync(user, new Claim(ClaimTypes.Role, "admin"));
            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserCredentials userCredentials)
        {
            var result = await _signInManager.PasswordSignInAsync(userCredentials.Email, userCredentials.Password, 
                isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                throw new UnauthorizedException(nameof(UserCredentials),"Invalid login attempt");
            }

            var user = await _userManager.FindByEmailAsync(userCredentials.Email);

            return Ok(BuildToken(userCredentials, user));
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser([FromBody] UserCredentials userCredentials)
        {
            var user = new IdentityUser { UserName = userCredentials.Email, Email = userCredentials.Email };

            var result = await _userManager.CreateAsync(user, userCredentials.Password);

            if (!result.Succeeded)
            {
                throw new BadRequestException("Username or password invalid");
            }
            return Ok(BuildToken(userCredentials, user));
        }

        private AuthenticationResponse BuildToken(UserCredentials userCredentials, IdentityUser user)
        {
            var roles = _userManager.GetRolesAsync(user).Result;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userCredentials.Email),
                new Claim(ClaimTypes.Email, userCredentials.Email),
            };
            

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            //var userClaims =  _userManager.GetClaimsAsync(user).Result;
            //claims.AddRange(userClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT")["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddYears(1);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            return new AuthenticationResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
