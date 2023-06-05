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
		private readonly UserManager<ApiUser> _userManager;

		public AuthManager(IMapper mapper, UserManager<ApiUser> userManager)
		{
			_mapper = mapper;
			_userManager = userManager;
		}

		public async Task<IEnumerable<IdentityError>> Register(ApiUserDto apiUserDto)
		{
			var user = _mapper.Map<ApiUser>(apiUserDto);
			user.UserName = apiUserDto.Email;

			var result = await _userManager.CreateAsync(user);

			if (result.Succeeded) await _userManager.AddToRoleAsync(user, RoleNames.User);

			return result.Errors;
		}
	}
}