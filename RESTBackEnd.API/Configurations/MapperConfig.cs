using AutoMapper;
using RESTBackEnd.API.Data;
using RESTBackEnd.API.Modes.Recipe;

namespace RESTBackEnd.API.Configurations
{
	public class MapperConfig : Profile
	{
		public MapperConfig()
		{
			CreateMap<Recipe, CreateRecipeDto>().ReverseMap();
		}

	}
}
