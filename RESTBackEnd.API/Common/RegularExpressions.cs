namespace RESTBackEnd.API.Common
{
	public static class RegularExpressions
	{
		public const string Password = "^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=])(?=\\S+$).{8,20}$";
	}
}