using Domain.VO;
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

        [HttpPost]
        [Route("/GetAllRequisicoes")]
        public async Task<ActionResult> GetAllRequisicoes([FromBody] WithLoginVO<Object> bodyVO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //var wsGP = new MXMWS_GestaoDeProcessos.MXMWS_GestaoDeProcessosClient();
                //var token = new MXMWS_GestaoDeProcessos.TUserProcessToken()
                //{
                //    User = bodyVO.Login.Usuario,
                //    Pw = bodyVO.Login.Senha,
                //    Amb = bodyVO.Login.Ambiente,
                //};

                //var teste = wsGP.AprovacoesObterRegistoAsync(token, bodyVO.Login.Usuario).Result;
                //var testn = wsGP.AprovacoesProcessaIntegracaoAsync(token, "").Result;
                return Ok(await _service.GetAllRequisicao());
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //[HttpPost]
        //[Route("/GetRequisicaoPorCodigo")]
        //public async Task<ActionResult> GetRequisicaoPorCodigo([FromBody] WithLoginVO<RequisicaoBaseVO> reqBodyVO)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }


        //    try
        //    {
        //        return Ok(await _service.GetRequisicaoporCodigo(reqBodyVO.Dados.ReqNumero));
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}

        [HttpPost]
        [Route("/GetItensRequisicao")]
        public async Task<ActionResult> GetItensRequisicao([FromBody] WithLoginVO<RequisicaoBaseVO> reqBodyVO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                return Ok(await _service.GetItemdaRequisicao(reqBodyVO.Dados.ReqNumero));
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("/GetAnexosRequisicao")]
        public async Task<ActionResult> GetAnexosRequisicao([FromBody] WithLoginVO<RequisicaoBaseVO> reqBodyVO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.GetAnexodaRequisicao(reqBodyVO.Dados.ReqNumero));
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("/AprovarRequisicao")]
        public async Task<ActionResult> AprovarRequisicao([FromBody] WithLoginVO<AprovacaoPendenteVO> reqBodyVO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok( true);
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
