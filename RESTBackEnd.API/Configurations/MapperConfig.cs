using AutoMapper;
using RESTBackEnd.API.Data;
using RESTBackEnd.API.Models.Ingredient;
using RESTBackEnd.API.Models.Recipe;
using RESTBackEnd.API.Models.UnitMeasure;

namespace RESTBackEnd.API.Configurations
{
	public class MapperConfig : Profile
	{
		public MapperConfig()
		{
			CreateMap<Recipe, CreateRecipeDto>().ReverseMap();
			CreateMap<Recipe, UpdateRecipeDto>().ReverseMap();
			CreateMap<Recipe, GetRecipeDto>().ReverseMap();
			CreateMap<Recipe, GetRecipeDetailDto>().ReverseMap();

			CreateMap<Ingredient, CreateIngredientDto>().ReverseMap();
			CreateMap<Ingredient, UpdateIngredientDto>().ReverseMap();
			CreateMap<Ingredient, GetIngredientDto>().ReverseMap();

			CreateMap<UnitMeasure, CreateUnitMeasureDto>().ReverseMap();
			CreateMap<UnitMeasure, UpdateUnitMeasureDto>().ReverseMap();
			CreateMap<UnitMeasure, GetUnitMeasureDto>().ReverseMap();
		}
	}
}