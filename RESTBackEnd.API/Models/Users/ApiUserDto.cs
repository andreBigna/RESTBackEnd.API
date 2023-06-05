#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
using System.ComponentModel.DataAnnotations;

namespace RESTBackEnd.API.Models.Users
{
	public class ApiUserDto
	{
		[Required, EmailAddress] public string Email { get; set; }

		[Required, DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.