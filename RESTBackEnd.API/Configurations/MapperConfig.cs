﻿using AutoMapper;
using RESTBackEnd.API.Data;
using RESTBackEnd.API.Models.Ingredient;
using RESTBackEnd.API.Models.Recipe;

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

			CreateMap<Ingredient, IngredientDto>().ReverseMap();
		}
	}
}