using RESTBackEnd.API.Data;

namespace RESTBackEnd.API.Interfaces
{
	public interface IRecipeRepository : IBaseRepository<Recipe>
	{
		Task<Recipe?> GetDetails(int id);
	}
}