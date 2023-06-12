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
		private readonly ILogger<UsersController> _logger;

		public UsersController(IAuthManager authManager, ILogger<UsersController> logger)
		{
			_authManager = authManager;
			_logger = logger;
		}

		[HttpPost, Route("register")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> Register([FromBody] IdentityUserDto user)
		{
			try
			{
				var errors = await _authManager.Register(user);

				var identityErrors = errors as IdentityError[] ?? errors.ToArray();
				if (!identityErrors.Any()) return Ok();
				foreach (var identityError in identityErrors)
				{
					ModelState.AddModelError(identityError.Code, identityError.Description);
				}

				return ReturnRegistrationErrors(user, identityErrors);
			}
			catch (Exception e)
			{
				return ReturnInternalError(e, nameof(Register));
			}
		}

		[HttpPost, Route("login")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> Login([FromBody] IdentityUserDto user)
		{
			try
			{
				var authResponse = await _authManager.Login(user);

				return authResponse == null ? BadRequest() : Ok(authResponse);
			}
			catch (Exception e)
			{
				return ReturnInternalError(e, nameof(Login));
			}
		}

		[HttpPost, Route("refreshtoken")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> RefreshToken([FromBody] AuthResponseDto authResponseDto)
		{
			try
			{
				var authResponse = await _authManager.VerifyRefreshToken(authResponseDto);

				return authResponse == null ? BadRequest() : Ok(authResponse);
			}
			catch (Exception e)
			{
				return ReturnInternalError(e, nameof(RefreshToken));
			}
		}

		internal IActionResult ReturnRegistrationErrors(IdentityUserDto user, IdentityError[] identityErrors)
		{
			var formattedErrors = string.Join(", ", identityErrors.Select(x => x.Code + " - " + x.Description));
			_logger.LogWarning(
				$"{nameof(UsersController)}.{nameof(Register)}, identity error for user {user.Email}: {formattedErrors}");

			return BadRequest();
		}

		internal IActionResult ReturnInternalError(Exception e, string methodName)
		{
			_logger.LogError(e, $"Error calling {nameof(UsersController)}.{methodName}");

			return Problem("Something went wrong, please contact support", statusCode: 500);
		}
	}
}