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
  public class SpicesController : ControllerBase
  {
    private readonly SpicesService _spicesService;
    public SpicesController(SpicesService spicesService)
    {
      _spicesService = spicesService;
    }
    [HttpGet]
    public ActionResult<List<Spice>> GetAll()
    {
      try
      {
        return Ok(_spicesService.GetAll());
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{spiceId}")]
    public ActionResult<Spice> GetById(int spiceId)
    {
      try
      {
        return Ok(_spicesService.GetById(spiceId));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [Authorize]
    [HttpPut("{spiceId}")]
    public async Task<ActionResult<Spice>> Update(int spiceId, [FromBody] Spice updatedSpice)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        updatedSpice.Id = spiceId;
        return _spicesService.Update(updatedSpice, userInfo.Id);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}