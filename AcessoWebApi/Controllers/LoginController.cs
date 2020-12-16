using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AcessoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<object> Login([FromBody] LoginDto loginDto, 
                                        [FromServices] ILoginService service)
        {
            if (!ModelState.IsValid || loginDto == null)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    var result = await service.FindByLogin(loginDto);
                    if (result != null) return Ok(result);
                    else return NotFound();
                }
                catch (ArgumentException e)
                {

                    return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
                }
            }
        } 

    }

}