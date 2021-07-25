using Domain.VO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace PadraoWebApi.Helpers
{
    public static class SOAPHelper
    {
        /// <summary>
        /// Sends a custom sync SOAP request to given URL and receive a request
        /// </summary>
        /// <param name="url">The WebService endpoint URL</param>
        /// <param name="action">The WebService action name</param>
        /// <returns>A string containing the raw Web Service response</returns>
        public static string PostSOAPRequest(string url, string action, LoginVO login, List<AprovacaoPendenteVO> dados)
        {

            //string url = "http://desktop-gl8smp9:8088/mockIMXMWS_GestaoDeProcessosbinding?WSDL";
            //string url = "http://localhost/wsmxm/MXMWS_GestaoDeProcessos.exe/soap/IMXMWS_GestaoDeProcessos";
            string urlWS = url ?? "https://192.168.100.36/webservicemxm/MXMWS_GestaoDeProcessos.exe/soap/IMXMWS_GestaoDeProcessos";
            string actionstr = action ?? "AprovacoesProcessaIntegracao";
            string SOAPAction = "urn:MXMWS_GestaoDeProcessosIntf-IMXMWS_GestaoDeProcessos#" + actionstr;

            string headerSoap = GetTokenUserProcessHeader(login);
            string body = GetBodyAprovacoesProcessaIntegracao(dados);
            string soapXml = $@"<?xml version=""1.0"" encoding=""UTF-8""?>                    
                    <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/""
                                      xmlns:urn=""urn:MXMWS_GestaoDeProcessosIntf-IMXMWS_GestaoDeProcessos""
                                      xmlns:urn1=""urn:MXMInvokable"">
                       <soapenv:Header>{headerSoap}</soapenv:Header>
                       <soapenv:Body>{body}</soapenv:Body>
                 </soapenv:Envelope>";
            return DoSoapRequest(urlWS, SOAPAction, soapXml);
        }

        private static string DoSoapRequest(string urlWS, string SOAPAction, string soapXml)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            var wr = (HttpWebRequest)WebRequest.Create(urlWS);
            byte[] buff = Encoding.UTF8.GetBytes(soapXml);
            wr.Timeout = 60000;
            wr.Method = "POST";
            wr.ContentType = "text/xml;charset=UTF-8";
            wr.ContentLength = buff.Length;
            wr.AutomaticDecompression = DecompressionMethods.GZip;
            wr.Headers.Add("SOAPAction", SOAPAction);

            using (var reqStream = wr.GetRequestStream())
            {
                reqStream.Write(buff, 0, buff.Length);
            }

            var resp = wr.GetResponse();
            var respStream = resp.GetResponseStream();
            var reader = new StreamReader(respStream, Encoding.UTF8);
            var resposta = reader.ReadToEnd();

            return resposta;
        }

        private static string GetBodyAprovacoesProcessaIntegracao(List<AprovacaoPendenteVO> dados)
        {
            var linhasAprov = getLinhasAprov(dados);

            return $@"
                <urn:AprovacoesProcessaIntegracao soapenv:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"">
                     <AXML xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:type=""xsd:string"">
                       <LinhaAprovLOCs>
                          {linhasAprov}  
                       </LinhaAprovLOCs>
                     </AXML>
                  </urn:AprovacoesProcessaIntegracao>
            ";
        }

        private static object getLinhasAprov(List<AprovacaoPendenteVO> dados)
        {
            var linhasAprovacoesXml = "";


            dados.ForEach(aprovacaoVO => linhasAprovacoesXml += getStringFromVo(aprovacaoVO));

            return linhasAprovacoesXml;
        }

        private static string getStringFromVo(AprovacaoPendenteVO aprovacaoVo)
        {

            var statusSpecifico = String.IsNullOrWhiteSpace(aprovacaoVo.StatusEspecifico) ?
                "<laaStatusEspecif/>"
                : $@"<laaStatusEspecif>{aprovacaoVo.StatusEspecifico}</laaStatusEspecif>";

            var linhaAprovLOC = $@"<LinhaAprovLOC>
                            <laaSQAprovacao>{aprovacaoVo.SequenciaAprovacao}</laaSQAprovacao>
                            <laaSQItemAprovacao>{aprovacaoVo.SequenciaItemAprovacao}</laaSQItemAprovacao>
                            <laaSQItemLinhaAprovacao>{aprovacaoVo.SequenciaItemLinhaAprovacao}</laaSQItemLinhaAprovacao>
                            <laaSequencia>{aprovacaoVo.Sequencia}</laaSequencia>                
                            <laaStatus>{aprovacaoVo.Status}</laaStatus>
                            {statusSpecifico}
                            <laaTPAprovacao>{aprovacaoVo.TipoAprovacao}</laaTPAprovacao>
                            <laaAprovador>{aprovacaoVo.Aprovador}</laaAprovador>     
                            <laaDTAprovacao>{aprovacaoVo.DataAprovacao}</laaDTAprovacao>
                            <laaJustificativa>{aprovacaoVo.Justificativa}</laaJustificativa>
                            <laaCDProcesso>{aprovacaoVo.CodigoProcesso}</laaCDProcesso>  
                          </LinhaAprovLOC>";
            return linhaAprovLOC;
        }

        public static string GetTokenUserProcessHeader(LoginVO login)
        {
            var ambiente = !String.IsNullOrWhiteSpace(login.SID) ? login.SID : login.ServiceName;
            return $@"
                <urn1:TUserProcessToken 
                    xmlns:urn=""urn:MXMInvokable""
                    xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                    xsi:type=""urn:TUserProcessToken"">
                         <User xsi:type=""xsd:string"">{login.Usuario}</User>
                         <Pw xsi:type=""xsd:string"">{login.Senha}</Pw>
                         <Token xsi:type=""xsd:string""/>
                         <Amb xsi:type=""xsd:string"">{ambiente}</Amb>
                         <MultiAmbiente xsi:type=""xsd:string"">{login.Usuario}</MultiAmbiente>
                         <PathTemp xsi:type=""xsd:string"">C:\TEMP</PathTemp>
                         <AlteraSecao xsi:type=""xsd:string"">S</AlteraSecao>
                </urn1:TUserProcessToken> 
            ";
            //return $@"
            //    <urn1:TUserProcessToken 
            //        xmlns:urn=""urn:MXMInvokable""
            //        xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
            //        xsi:type=""urn:TUserProcessToken"">
            //             <User xsi:type=""xsd:string"">HOM_SHP</User>
            //             <Pw xsi:type=""xsd:string"">HOM_SHP</Pw>
            //             <Token xsi:type=""xsd:string""/>
            //             <Amb xsi:type=""xsd:string"">HOM</Amb>
            //             <MultiAmbiente xsi:type=""xsd:string"">HOM_SHP</MultiAmbiente>
            //             <PathTemp xsi:type=""xsd:string"">C:\TEMP</PathTemp>
            //             <AlteraSecao xsi:type=""xsd:string"">S</AlteraSecao>
            //    </urn1:TUserProcessToken> 
            //";
        }

        public static T GetXMLGenericType<T>(string xml) { 


            //var xml = @"<Students><Student><Name>Arul</Name><Mark>90</Mark></Student></Students>";

            var serializer = new XmlSerializer(typeof(T));

            var students = (T)serializer.Deserialize(new XmlTextReader(new StringReader(xml)));
            //XmlSerializer serializer = new XmlSerializer(typeof(T));
            //    var generatedType = (T)serializer.Deserialize(new StreamReader(xmlFile));
            return (T)Convert.ChangeType(students, typeof(T));

        }
    }
}
