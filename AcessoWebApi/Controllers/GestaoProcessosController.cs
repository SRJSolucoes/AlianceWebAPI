using Data.Handlers;
using Domain.VO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PadraoWebApi.Helpers;
using PadraoWebApi.XML;
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

        private string defaultToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";

        public GestaoProcessosController(IShoppingService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("/GetAllRequisicoes")]
        public async Task<ActionResult> GetAllRequisicoes([FromBody] WithLoginVO<UsuarioVO> bodyVO)
        {
            if (bodyVO.Token != defaultToken)
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

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

            if (reqBodyVO.Token != defaultToken)
            {
                return Unauthorized();
            }

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
            if (reqBodyVO.Token != defaultToken)
            {
                return Unauthorized();
            }

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
            if (reqBodyVO.Token != defaultToken)
            {
                return Unauthorized();
            }

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
            if (reqBodyVO.Token != defaultToken)
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // TODO Mechi aqui para ajustar o Login
                LoginVO Login       = new LoginVO();
                Login.Usuario = "HOM_SHP";
                Login.Senha = "HOM_SHP";
                Login.Host = "10.0.100.23";
                Login.ServiceName = "HOM";
                Login.Port = "1521";

                string url = "https://192.168.100.36/webservicemxm/MXMWS_GestaoDeProcessos.exe/soap/IMXMWS_GestaoDeProcessos";
                var action = "urn:MXMWS_GestaoDeProcessosIntf-IMXMWS_GestaoDeProcessos#AprovacoesProcessaIntegracao";
                //var xmlRetorno = SOAPHelper.PostSOAPRequest(url, action, reqBodyVO.Login, reqBodyVO.Dados);
                var xmlRetorno = SOAPHelper.PostSOAPRequest(url, action, Login, reqBodyVO.Dados);

                //var teste = SOAPHelper.GetXMLGenericType<WebServiceReturnTeste>(resposta);

                //WebServiceReturnService service = new WebServiceReturnService();
                //service.ProcessWebServiceReturn(xmlRetorno);

                //xmlRetorno = $@"<SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/""
                //   xmlns:xsd=""http://www.w3.org/2001/XMLSchema""
                //   xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                //   xmlns:SOAP-ENC=""http://schemas.xmlsoap.org/soap/encoding/"">
                //   <SOAP-ENV:Body SOAP-ENC:encodingStyle=""http://schemas.xmlsoap.org/soap/envelope/"">
                //      <NS1:AprovacoesProcessaIntegracaoResponse xmlns:NS1=""urn:MXMWS_GestaoDeProcessosIntf-IMXMWS_GestaoDeProcessos"">
                //         <return xsi:type=""xsd:string"">&lt;WebServiceReturn&gt;
                //&lt; Atributos / &gt;
                //&lt; ErroMSGs / &gt;
                //&lt; Registros &gt;
                //&lt; Registro &gt;
                //&lt; wsrrSequencial &gt; 1 &lt;/ wsrrSequencial &gt;
                //&lt; wsrrTipo &gt; LinhaAprovLOC &lt;/ wsrrTipo &gt;
                //&lt; Campos &gt;
                //&lt; Campo &gt;
                //&lt; wsrcNome &gt; LAU_SQAPROVACAO &lt;/ wsrcNome &gt;
                //&lt; wsrcValor &gt; 1230151 &lt;/ wsrcValor &gt;
                //&lt;/ Campo &gt;
                //&lt; Campo &gt;
                //&lt; wsrcNome &gt; LAU_SQAPROVACAO &lt;/ wsrcNome &gt;
                //&lt; wsrcValor &gt; 1230151 &lt;/ wsrcValor &gt;
                //&lt;/ Campo &gt;
                //&lt; Campo &gt;
                //&lt; wsrcNome &gt; LAU_SQAPROVACAO &lt;/ wsrcNome &gt;
                //&lt; wsrcValor &gt; 1230151 &lt;/ wsrcValor &gt;
                //&lt;/ Campo &gt;
                //&lt; Campo &gt;
                //&lt; wsrcNome &gt; LAU_SQAPROVACAO &lt;/ wsrcNome &gt;
                //&lt; wsrcValor &gt; 1230151 &lt;/ wsrcValor &gt;
                //&lt;/ Campo &gt;
                //&lt;/ Campos &gt;
                //&lt;/ Registro &gt;
                //&lt;/ Registros &gt;
                //&lt;/ WebServiceReturn &gt;
                //</return>
                //      </NS1:AprovacoesProcessaIntegracaoResponse>
                //   </SOAP-ENV:Body>
                //</SOAP-ENV:Envelope>";


                if (XmlHasCampo(xmlRetorno, "ErroMSGs") && XmlHasCampo(xmlRetorno, "ermErro"))
                {
                    var erroWS = new
                    {
                        Messagem = "O WebService retornou erros",
                        Erros = new
                        {
                            CDErro = getCampoFromXml(xmlRetorno, "ermCDErro"),
                            DSErro = getCampoFromXml(xmlRetorno, "ermDSErro", false),
                            MensagemAux = getCampoFromXml(xmlRetorno, "ermMensagemAux", false),
                            Erro = getCampoFromXml(xmlRetorno, "ermErro"),
                        }
                    };
                    return StatusCode((int)HttpStatusCode.BadRequest, erroWS);

                }

                if (XmlHasCampo(xmlRetorno, "Registro") && XmlHasCampo(xmlRetorno, "Campo"))
                {
                    var camposStr = getCampoFromXml(xmlRetorno, "Campos");
                    //var decoded = System.Web.HttpUtility.HtmlDecode(camposStr);
                    var decoded = $@"<?xml version=""1.0"" encoding=""utf-8"" ?><Campos>" 
                            + System.Web.HttpUtility.HtmlDecode(camposStr.Replace(" ", "")) + "</Campos>";
                    var listaCampos = SOAPHelper.GetXMLGenericType<Campos>(decoded).Campo;
                    //System.Web.HttpUtility.HtmlDecode(xmlRetorno.Replace(" ", ""))

                    var SuccessWS = new
                    {
                        wsrrSequencial = getCampoFromXml(xmlRetorno, "wsrrSequencial"),
                        wsrrTipo = getCampoFromXml(xmlRetorno, "wsrrTipo"),
                        campos = listaCampos
                    };
                    return StatusCode((int)HttpStatusCode.OK, SuccessWS);
                }

                return StatusCode((int)HttpStatusCode.BadRequest, new { Error = "Algo falhou na comunicação com o WSMXM" });
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private static string getCampoFromXml(string xmlRetorno, string campo, bool removeSpace = true)
        {
            var campoStr = xmlRetorno;
            if (removeSpace)
            {
                campoStr = xmlRetorno.Replace(" ", "");
            }
            return campoStr.Split("&lt;" + campo + "&gt;")[1].Split("&lt;/" + campo + "&gt;")[0];
            //return xmlRetorno.Split(campo)[1].Replace("&gt;", "").Replace("&lt;/", "");
        }
        private static bool XmlHasCampo(string xmlRetorno, string campo)
        {
            return xmlRetorno.Contains(campo, StringComparison.Ordinal);
        }

        [Serializable()]
        public class Campo
        {
            public string wsrcNome { get; set; }
            public string wsrcValor { get; set; } 
        }

        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class Campos
        {
            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Campo")]
            public Campo[] Campo { get; set; }
        }
    }
}
