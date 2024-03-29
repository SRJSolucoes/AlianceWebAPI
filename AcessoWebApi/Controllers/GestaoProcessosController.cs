using Domain.Config;
using AcessoWebApi.Infrastructure.Security;
using Domain.VO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PadraoWebApi.Helpers;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace PadraoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestaoProcessosController : ControllerBase
    {
        private IShoppingService _service;
        private AlianceApiSettings _appSettings;
        private ICurrentUserAccessor _currentUserAccessor;

        //private string defaultToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";

        public GestaoProcessosController(
            IShoppingService service,
            ICurrentUserAccessor currentUserAccessor,
            IOptionsSnapshot<AlianceApiSettings>  appSettings,
            IConfiguration configuration
        )
        {
            _service = service;
            _currentUserAccessor = currentUserAccessor;            
            _appSettings = appSettings.Value;
            AlianceApiSettings.ConfigurarSoDatabaseVariables(_appSettings, configuration);
        }

        [HttpPost]
        [Route("/GetAllRequisicoes")]
        public async Task<ActionResult> GetAllRequisicoes([FromBody] WithTokenVO<UsuarioVO> bodyVO)
        {
            if (bodyVO.Token != _appSettings.TokenDefault)
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
        public async Task<ActionResult> GetItensRequisicao([FromBody] WithTokenVO<ItensRequisicaoVO> reqBodyVO)
        {

            if (reqBodyVO.Token != _appSettings.TokenDefault)
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
        public async Task<ActionResult> GetAnexosRequisicao([FromBody] WithTokenVO<RequisicaoBaseVO> reqBodyVO)
        {
            if (reqBodyVO.Token != _appSettings.TokenDefault)
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
        public async Task<ActionResult> GetDetReqPagamento([FromBody] WithTokenVO<DetReqPagamentoVO> reqBodyVO)
        {
            if (reqBodyVO.Token != _appSettings.TokenDefault)
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
        public async Task<ActionResult> AprovarRequisicao([FromBody] WithTokenVO<List<AprovacaoPendenteVO>> reqBodyVO)
        {
            if (reqBodyVO.Token != _appSettings.TokenDefault)
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // TODO Falta terminar a implementação
                // LoginVO Login = _currentUserAccessor.GetMXMLoginFromToken();

                LoginVO Login = new LoginVO()
                {
                    Usuario = _appSettings.SODatabaseVariables.ActiveDBfromSO ? _appSettings.DatabaseConfigFromSO.Usuario : _appSettings.DatabaseConfig.Usuario,
                    Senha = _appSettings.SODatabaseVariables.ActiveDBfromSO ? _appSettings.DatabaseConfigFromSO.Senha : _appSettings.DatabaseConfig.Senha,
                    Host = _appSettings.SODatabaseVariables.ActiveDBfromSO ? _appSettings.DatabaseConfigFromSO.Host : _appSettings.DatabaseConfig.Host,
                    ServiceName = _appSettings.SODatabaseVariables.ActiveDBfromSO ? _appSettings.DatabaseConfigFromSO.ServiceName : _appSettings.DatabaseConfig.ServiceName,
                    SID = _appSettings.SODatabaseVariables.ActiveDBfromSO ? _appSettings.DatabaseConfigFromSO.SID : _appSettings.DatabaseConfig.SID,
                    AmbWs = _appSettings.SODatabaseVariables.ActiveDBfromSO ? _appSettings.DatabaseConfigFromSO.AmbWs : _appSettings.DatabaseConfig.AmbWs,
                    Port = _appSettings.SODatabaseVariables.ActiveDBfromSO ? _appSettings.DatabaseConfigFromSO.Port : _appSettings.DatabaseConfig.Port
                };


                //string url = "https://192.168.100.36/webservicemxm/MXMWS_GestaoDeProcessos.exe/soap/IMXMWS_GestaoDeProcessos";
                string url = _appSettings.WSGestaoProcessosettings.Url;
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
                    var infoBD = Login;
                    infoBD.Senha = "";
                    var headerRequest = String.Join(" ", Regex.Split(SOAPHelper.GetTokenUserProcessHeader(infoBD), @"(?:\r\n|\n|\r)"));

                    var erroWS = new
                    {
                        Messagem = "O WebService retornou erros",
                        Erros = new
                        {
                            HeaderBody = headerRequest,
                            CDErro = getCampoFromXml(xmlRetorno, "ermCDErro"),
                            DSErro = getCampoFromXml(xmlRetorno, "ermDSErro", false),
                            MensagemAux = getCampoFromXml(xmlRetorno, "ermMensagemAux", false),
                            Erro = getCampoFromXml(xmlRetorno, "ermErro"),
                        }
                    };
                    return StatusCode((int)HttpStatusCode.BadRequest, erroWS);

                }

                if (XmlHasCampo(xmlRetorno, "faultcode") && XmlHasCampo(xmlRetorno, "faultstring"))
                {
                    var infoBD = Login;
                    infoBD.Senha = "";

                    var headerRequest = String.Join(" ", Regex.Split(SOAPHelper.GetTokenUserProcessHeader(infoBD), @"(?:\r\n|\n|\r)"));

                    var erroWS = new
                    {
                        Messagem = "O WebService retornou erros",
                        Erros = new
                        {

                            HeaderBody = headerRequest,
                            CDErro = getCampoFromXml(xmlRetorno, "faultcode"),
                            Erro = getCampoFromXml(xmlRetorno, "faultstring", false),
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
            var info = "";
            try
            {
                info = campoStr.Split("&lt;" + campo + "&gt;")[1].Split("&lt;/" + campo + "&gt;")[0];
            }
            catch
            {
                info = campoStr.Split("<" + campo + ">")[1].Split("</" + campo + ">")[0];
            }

            return info;
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
