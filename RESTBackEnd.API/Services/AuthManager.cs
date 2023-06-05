using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RESTBackEnd.API.Common;
using RESTBackEnd.API.Interfaces;
using RESTBackEnd.API.Models.Users;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace RESTBackEnd.API.Services
{
	public class AuthManager : IAuthManager
	{
		private readonly IMapper _mapper;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IConfiguration _configuration;

		public AuthManager(IMapper mapper, UserManager<IdentityUser> userManager, IConfiguration configuration)
		{
			_mapper = mapper;
			_userManager = userManager;
			_configuration = configuration;
		}

		public async Task<IEnumerable<IdentityError>> Register(IdentityUserDto identityUserDto)
		{
			var user = _mapper.Map<IdentityUser>(identityUserDto);
			user.UserName = identityUserDto.Email;

			var result = await _userManager.CreateAsync(user, identityUserDto.Password);

			if (result.Succeeded) await _userManager.AddToRoleAsync(user, RoleNames.User);

			return result.Errors;
		}

		public async Task<AuthResponse?> Login(IdentityUserDto identityUserDto)
		{
			var user = await _userManager.FindByEmailAsync(identityUserDto.Email);
			if (user == null) return null;

			var isValidPassword = await _userManager.CheckPasswordAsync(user, identityUserDto.Password);
			if (!isValidPassword) return null;

			var token = await GenerateToken(user);

			return new AuthResponse() { Token = token, UserId = user.Id };
		}

		private async Task<string> GenerateToken(IdentityUser user)
		{
			var roles = await _userManager.GetRolesAsync(user);
			var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
			var userClaims = await _userManager.GetClaimsAsync(user);

			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Email, user.Email!),
				new Claim("uid", user.Id)
			}.Union(roleClaims).Union(userClaims);


			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!));
			var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
			var durationInMinutes = Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"]);

			var token = new JwtSecurityToken(
				issuer: _configuration["JwtSettings:Issuer"],
				audience: _configuration["JwtSettings:Audience"],
				claims: claims,
				expires: DateTime.Now.AddMinutes(durationInMinutes),
				signingCredentials: signingCredentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}