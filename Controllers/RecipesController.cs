using System.Collections.Generic;
using System.Threading.Tasks;
using AllSpice.Models;
using AllSpice.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllSpice.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class RecipesController : ControllerBase
  {
    private readonly RecipesService _recipesService;
    private readonly SpicesService _spicesService;
    public RecipesController(RecipesService recipesServices, SpicesService spicesService)
    {
      _recipesService = recipesServices;
      _spicesService = spicesService;
    }
    [HttpGet]
    public ActionResult<List<RecipesController>> GetAll()
    {
      try
      {
        return Ok(_recipesService.GetAll());
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{recipeId}")]
    public ActionResult<Recipe> GetById(int recipeId)
    {
      try
      {
        return Ok(_recipesService.GetById(recipeId));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{recipeId}/spices")]
    public ActionResult<List<Spice>> GetSpicesByRecipe(int recipeId)
    {
      try
      {
        return Ok(_spicesService.GetSpicesByRecipe(recipeId));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Recipe>> Post([FromBody] Recipe recipeData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        recipeData.CreatorId = userInfo.Id;
        Recipe createdRecipe = _recipesService.Post(recipeData);
        createdRecipe.Writer = userInfo;
        return createdRecipe;
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpDelete("{recipeId}")]
    public async Task<ActionResult<string>> RemoveRecipe(int recipeId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _recipesService.RemoveRecipe(recipeId, userInfo.Id);
        return Ok("Deleted Recipe");
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}