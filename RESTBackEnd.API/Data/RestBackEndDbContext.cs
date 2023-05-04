using Microsoft.EntityFrameworkCore;

namespace RESTBackEnd.API.Data
{
	public class RestBackEndDbContext: DbContext
	{
		public RestBackEndDbContext(DbContextOptions options): base(options)
		{

		}
	}
}
