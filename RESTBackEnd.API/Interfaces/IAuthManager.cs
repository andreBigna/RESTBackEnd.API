using Microsoft.AspNetCore.Identity;
using RESTBackEnd.API.Models.Users;

namespace RESTBackEnd.API.Interfaces
{
	public interface IAuthManager
	{
		Task<IEnumerable<IdentityError>> Register(IdentityUserDto identityUserDto);

		Task<bool> Login(IdentityUserDto identityUserDto);
	}
}