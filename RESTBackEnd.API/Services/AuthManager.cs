using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RESTBackEnd.API.Common;
using RESTBackEnd.API.Data;
using RESTBackEnd.API.Interfaces;
using RESTBackEnd.API.Models.Users;

namespace RESTBackEnd.API.Services
{
	public class AuthManager : IAuthManager
	{
		private readonly IMapper _mapper;
		private readonly UserManager<IdentityUser> _userManager;

		public AuthManager(IMapper mapper, UserManager<IdentityUser> userManager)
		{
			_mapper = mapper;
			_userManager = userManager;
		}

		public async Task<IEnumerable<IdentityError>> Register(IdentityUserDto identityUserDto)
		{
			var user = _mapper.Map<IdentityUser>(identityUserDto);
			user.UserName = identityUserDto.Email;

			var result = await _userManager.CreateAsync(user);

			if (result.Succeeded) await _userManager.AddToRoleAsync(user, RoleNames.User);

			return result.Errors;
		}

		public async Task<bool> Login(IdentityUserDto identityUserDto)
		{
			var user = await _userManager.FindByEmailAsync(identityUserDto.Email);
			if (user == null) return default;

			var isValidPassword = await _userManager.CheckPasswordAsync(user, identityUserDto.Password);
			return isValidPassword;
		}
	}
}