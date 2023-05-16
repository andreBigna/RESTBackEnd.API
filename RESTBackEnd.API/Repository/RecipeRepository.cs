using Microsoft.EntityFrameworkCore;
using RESTBackEnd.API.Data;
using RESTBackEnd.API.Interfaces;

namespace RESTBackEnd.API.Repository
{
	public class RecipeRepository : BaseRepository<Recipe>, IRecipeRepository
	{
		private readonly RestBackEndDbContext _context;

		public RecipeRepository(RestBackEndDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<Recipe?> GetDetails(int id)
		{
			return await _context.Recipes.Include(r => r.Ingredients).FirstOrDefaultAsync(r => r.RecipeId == id);
		}
	}
}