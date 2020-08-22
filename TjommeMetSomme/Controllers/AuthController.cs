using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TjommeMetSomme.Entities;
using TjommeMetSomme.Entities.Identity;
using TjommeMetSomme.Resources;
using TjommeMetSomme.Settings;

namespace TjommeMetSomme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser>_userManager;

        private readonly RoleManager<ApplicationRole> _roleManager;

        private readonly JwtSettings _jwtSettings;

        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _userManager = userManager;

            _roleManager = roleManager;

            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInResource signInResource)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Email == signInResource.Email);

            if (user is null)
            {
                return NotFound("User not found");
            }

            var userSigninResult = await _userManager.CheckPasswordAsync(user, signInResource.Password);

            if (userSigninResult)
            {
                var roles = await _userManager.GetRolesAsync(user);

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));

                claims.AddRange(roleClaims);

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpirationInDays));

                var token = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Issuer,
                    claims,
                    expires: expires,
                    signingCredentials: creds
                );

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            return BadRequest("Email or password incorrect.");
        }

        [HttpGet("User")]
        [Authorize]
        public async Task<IActionResult> GetUserFromJWT()
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (User is null)
            {
                return NotFound("User not found");
            }

            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id.Equals(Guid.Parse(userId))).ConfigureAwait(false);

            if (user is null)
            {
                return NotFound("User not found");
            }

            return Ok(new { user });
        }

        [HttpPost("Roles")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest("Role name should be provided.");
            }

            var newRole = new ApplicationRole
            {
                Name = roleName
            };

            var roleResult = await _roleManager.CreateAsync(newRole);

            if (roleResult.Succeeded)
            {
                return Ok();
            }

            return Problem(roleResult.Errors.First().Description, null, 500);
        }
    }
}