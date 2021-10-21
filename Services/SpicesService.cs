using System;
using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AllSpice.Services
{
  public class SpicesService
  {
    private readonly SpicesRepository _spicesRepository;

    public SpicesService(SpicesRepository spicesRepository)
    {
      _spicesRepository = spicesRepository;
    }
    public List<Spice> GetAll()
    {
      return _spicesRepository.GetAll();
    }
    public Spice GetById(int spiceId)
    {
      Spice foundSpice = _spicesRepository.GetById(spiceId);
      if (foundSpice == null)
      {
        throw new Exception("No spice by that Id");
      }
      return foundSpice;
    }
    public List<Spice> GetSpicesByRecipe(int recipeId)
    {
      return _spicesRepository.GetSpicesByRecipe(recipeId);
    }
    internal ActionResult<Spice> Update(Spice updatedSpice, string id)
    {
      Spice foundSpice = GetById(updatedSpice.Id);
      if (foundSpice.RecipeId != null)
      {
        throw new Exception("Not Allowed");
      }
      foundSpice.RecipeId = updatedSpice.RecipeId;
      return _spicesRepository.Update(foundSpice);
    }
    internal object GetSpicesByAccount(string id)
    {
      return _spicesRepository.GetSpicesByAccount(id);
    }
  }
}