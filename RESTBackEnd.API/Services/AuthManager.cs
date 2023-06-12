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
		private readonly ILogger<AuthManager> _logger;
		private readonly string _issuer;
		private const string RefreshToken = "RefreshToken";


		public AuthManager(IMapper mapper, UserManager<IdentityUser> userManager, IConfiguration configuration,
			ILogger<AuthManager> logger)
		{
			_mapper = mapper;
			_userManager = userManager;
			_configuration = configuration;
			_logger = logger;
			_issuer = _configuration["JwtSettings:Issuer"]!;
		}

		public async Task<IEnumerable<IdentityError>> Register(IdentityUserDto identityUserDto)
		{
			var user = _mapper.Map<IdentityUser>(identityUserDto);
			user.UserName = identityUserDto.Email;

			var result = await _userManager.CreateAsync(user, identityUserDto.Password);

			if (result.Succeeded) await _userManager.AddToRoleAsync(user, RoleNames.User);

			return result.Errors;
		}

		public async Task<AuthResponseDto?> Login(IdentityUserDto identityUserDto)
		{
			var user = await _userManager.FindByEmailAsync(identityUserDto.Email);
			if (user == null) return ReturnCannotFindUserByEmail(identityUserDto);

			var isValidPassword = await _userManager.CheckPasswordAsync(user, identityUserDto.Password);
			if (!isValidPassword) return ReturnInvalidPassword(identityUserDto);

			var token = await GenerateToken(user);

			return new AuthResponseDto()
				{ Token = token, UserId = user.Id, RefreshToken = await CreateRefreshToken(identityUserDto) };
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

		public async Task<string> CreateRefreshToken(IdentityUserDto identityUserDto)
		{
			var user = await _userManager.FindByEmailAsync(identityUserDto.Email);

			await _userManager.RemoveAuthenticationTokenAsync(user!, _issuer, RefreshToken);

			var refreshedToken = await _userManager.GenerateUserTokenAsync(user!, _issuer, RefreshToken);

			await _userManager.SetAuthenticationTokenAsync(user!, _issuer, RefreshToken, refreshedToken);

			return refreshedToken;
		}

		public async Task<AuthResponseDto?> VerifyRefreshToken(AuthResponseDto authResponse)
		{
			if (string.IsNullOrWhiteSpace(authResponse.UserId) ||
			    string.IsNullOrWhiteSpace(authResponse.RefreshToken)) return ReturnInvalidUserIdOrToken();

			var user = await _userManager.FindByIdAsync(authResponse.UserId);
			if (user == null || string.IsNullOrWhiteSpace(user.Email) || user.Id != authResponse.UserId)
				return ReturnInvalidUserOrEmail(user);

			var isTokenValid =
				await _userManager.VerifyUserTokenAsync(user, _issuer, RefreshToken, token: authResponse.RefreshToken);

			if (isTokenValid)
			{
				var token = await GenerateToken(user);
				return new AuthResponseDto()
				{
					Token = token,
					UserId = authResponse.UserId,
					RefreshToken = await CreateRefreshToken(new IdentityUserDto()
						{ Password = string.Empty, Email = user.Email })
				};
			}

			await _userManager.UpdateSecurityStampAsync(user);
			_logger.LogWarning(
				$"{nameof(AuthManager)}.{nameof(VerifyRefreshToken)}: invalid {nameof(authResponse.RefreshToken)}");
			return null;
		}

		internal AuthResponseDto? ReturnCannotFindUserByEmail(IdentityUserDto identityUserDto)
		{
			_logger.LogWarning(
				$"{nameof(AuthManager)}.{nameof(Login)}: couldn't find user by email {identityUserDto.Email}");
			return null;
		}

		private AuthResponseDto? ReturnInvalidPassword(IdentityUserDto identityUserDto)
		{
			_logger.LogWarning($"{nameof(AuthManager)}.{nameof(Login)}: invalid password for {identityUserDto.Email}");
			return null;
		}

		private AuthResponseDto? ReturnInvalidUserIdOrToken()
		{
			_logger.LogWarning(
				$"{nameof(AuthManager)}.{nameof(VerifyRefreshToken)}: invalid {nameof(AuthResponseDto.UserId)} or {nameof(AuthResponseDto.RefreshToken)}");
			return null;
		}

		private AuthResponseDto? ReturnInvalidUserOrEmail(IdentityUser? user)
		{
			_logger.LogWarning(
				$"{nameof(AuthManager)}.{nameof(VerifyRefreshToken)}: invalid {nameof(user)} or {nameof(user.Email)}");
			return null;
		}
	}
}