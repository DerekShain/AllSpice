using System;
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
  [Route("[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly AccountService _accountService;
    private readonly RecipesService _recipesService;
    private readonly SpicesService _spicesService;

    public AccountController(AccountService accountService, RecipesService recipesService, SpicesService spicesService)
    {
      _accountService = accountService;
      _recipesService = recipesService;
      _spicesService = spicesService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountService.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpGet("recipes")]
    public async Task<ActionResult<List<Recipe>>> GetRecipesByAccount()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_recipesService.GetRecipesByAccount(userInfo.Id));
      }
      catch (System.Exception error)
      {
        return BadRequest(error.Message);
      }
    }

    [Authorize]
    [HttpGet("spices")]
    public async Task<ActionResult<List<Spice>>> GetSpicesByAccount()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_spicesService.GetSpicesByAccount(userInfo.Id));
      }
      catch (System.Exception error)
      {
        return BadRequest(error.Message);
      }
    }
  }


}