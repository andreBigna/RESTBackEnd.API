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
            CreateMap<Recipe, GetRecipeDto>().ReverseMap();
            CreateMap<Recipe, GetRecipeDetailDto>().ReverseMap();

            CreateMap<Ingredient, GetIngredientDto>().ReverseMap();
            CreateMap<Ingredient, CreateIngredientDto>().ReverseMap();

            CreateMap<UnitMeasure, GetUnitMeasureDto>().ReverseMap();
        }

    }
}
