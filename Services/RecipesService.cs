using System;
using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
  public class RecipesService
  {
    private readonly RecipesRepository _recipesRepository;
    public RecipesService(RecipesRepository recipesRepository)
    {
      _recipesRepository = recipesRepository;
    }
    public List<Recipe> GetAll()
    {
      return _recipesRepository.GetAll();
    }
    public Recipe GetById(int recipeId)
    {
      Recipe foundRecipe = _recipesRepository.GetById(recipeId);
      if (foundRecipe == null)
      {
        throw new Exception("Can't find Recipe");
      }
      return foundRecipe;
    }
    public Recipe Post(Recipe recipeData)
    {
      return _recipesRepository.Post(recipeData);
    }
    public void RemoveRecipe(int recipeId, string userId)
    {
      Recipe foundRecipe = GetById(recipeId);
      if (foundRecipe.CreatorId != userId)
      {
        throw new Exception("Not Allowed");
      }
      _recipesRepository.RemoveRecipe(recipeId);
    }
    public List<Recipe> GetRecipesByAccount(string userId)
    {
      return _recipesRepository.GetRecipesByAccount(userId);
    }
  }
}