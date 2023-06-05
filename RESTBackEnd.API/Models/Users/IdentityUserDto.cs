#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
using System.ComponentModel.DataAnnotations;
using RESTBackEnd.API.Common;

namespace RESTBackEnd.API.Models.Users
{
	public class IdentityUserDto
	{
		[Required, EmailAddress] public string Email { get; set; }

		[Required, RegularExpression(RegularExpressions.Password, ErrorMessage = "Invalid password")]
		public string Password { get; set; }
	}
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.