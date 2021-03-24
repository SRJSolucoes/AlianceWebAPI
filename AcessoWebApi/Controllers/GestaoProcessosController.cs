using Domain.VO;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;


namespace PadraoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestaoProcessosController : ControllerBase
    {
        private IShoppingService _service;

        public GestaoProcessosController(IShoppingService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("/GetAllRequisicoes")]
        public async Task<ActionResult> GetAllRequisicoes([FromBody] WithLoginVO<UsuarioVO> bodyVO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //var teste = new IMXMWS_GestaoDeProcessosservice().
                //var wsGP = new MXMWS_GestaoDeProcessos.MXMWS_GestaoDeProcessosClient();
                //var token = new MXMWS_GestaoDeProcessos.TUserProcessToken()
                //{
                //    User = bodyVO.Login.Usuario,
                //    Pw = bodyVO.Login.Senha,
                //    Amb = "(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.0.5)(PORT=1525)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=MXMDS9)))",
                //};

                //var teste = wsGP.AprovacoesObterRegistoAsync(token, bodyVO.Login.Usuario).Result;
                //var testn = wsGP.AprovacoesProcessaIntegracaoAsync(token, "").Result;
                return Ok(await _service.GetAllRequisicao(bodyVO.Dados.UsuarioEmail));
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
        public async Task<ActionResult> GetItensRequisicao([FromBody] WithLoginVO<ItensRequisicaoVO> reqBodyVO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                return Ok(await _service.GetItemdaRequisicao(reqBodyVO.Dados.ReqNumero, reqBodyVO.Dados.UsuarioNome));
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
        [Route("/GetDetReqPagamento")]
        public async Task<ActionResult> GetDetReqPagamento([FromBody] WithLoginVO<DetReqPagamentoVO> reqBodyVO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.GetDetReqPagamento(reqBodyVO.Dados.ReqNumero, reqBodyVO.Dados.CdFornecedor));
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("/AprovarRequisicao")]
        public async Task<ActionResult> AprovarRequisicao([FromBody] WithLoginVO<List<AprovacaoPendenteVO>> reqBodyVO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(new { Success = "Aprovações registradas com sucesso" });
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
