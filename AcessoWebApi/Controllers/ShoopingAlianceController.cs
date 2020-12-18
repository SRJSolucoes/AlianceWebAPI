using Domain.DTOs;
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
        [Route("/GetAllRequisicao")]
        public async Task<ActionResult> GetAllRequisicao([FromBody] MXMLoginDTO MXMLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.GetAllRequisicao());
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("/GetRequisicaoporCodigo")]
        public async Task<ActionResult> GetRequisicaoporCodigo([FromHeader] int Requisicao, [FromBody] MXMLoginDTO MXMLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.GetRequisicaoporCodigo(Requisicao));
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("/GetItemdaRequisicao")]
        public async Task<ActionResult> GetItemdaRequisicao([FromHeader] int Requisicao, [FromBody] MXMLoginDTO MXMLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.GetItemdaRequisicao(Requisicao));
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("/GetAnexodaRequisicao")]
        public async Task<ActionResult> GetAnexodaRequisicao([FromHeader] int Requisicao, [FromBody] MXMLoginDTO MXMLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.GetAnexodaRequisicao(Requisicao));
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
