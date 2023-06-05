using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RESTBackEnd.API.Interfaces;
using RESTBackEnd.API.Models.Users;

namespace RESTBackEnd.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IAuthManager _authManager;

		public UsersController(IAuthManager authManager)
		{
			_authManager = authManager;
		}

		[HttpPost, Route("register")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> Register([FromBody] IdentityUserDto user)
		{
			var errors = await _authManager.Register(user);

			var identityErrors = errors as IdentityError[] ?? errors.ToArray();
			if (!identityErrors.Any()) return Ok();
			foreach (var identityError in identityErrors)
			{
				ModelState.AddModelError(identityError.Code, identityError.Description);
			}

			return BadRequest();
		}

		[HttpPost, Route("login")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> Login([FromBody] IdentityUserDto user)
		{
			var loggedIn = await _authManager.Login(user);

			return loggedIn ? Ok() : BadRequest();
		}
	}
}