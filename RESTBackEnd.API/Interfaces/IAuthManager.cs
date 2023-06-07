using Microsoft.AspNetCore.Identity;
using RESTBackEnd.API.Models.Users;

namespace RESTBackEnd.API.Interfaces
{
	public interface IAuthManager
	{
		Task<IEnumerable<IdentityError>> Register(IdentityUserDto identityUserDto);

		Task<AuthResponseDto?> Login(IdentityUserDto identityUserDto);

		Task<string> CreateRefreshToken(IdentityUserDto identityUserDto);

		Task<AuthResponseDto?> VerifyRefreshToken(AuthResponseDto authResponseDto);
	}
}