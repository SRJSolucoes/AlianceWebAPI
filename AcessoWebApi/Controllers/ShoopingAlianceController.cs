using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System;
using System.Net;
using System.Threading.Tasks;


namespace PadraoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoopingAlianceController : ControllerBase
    {
        private IShoopingService _service;

        public ShoopingAlianceController(IShoopingService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.GetAll());
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
