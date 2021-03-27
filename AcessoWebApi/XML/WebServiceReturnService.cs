using PadraoWebApi.Interfaces;
using PadraoWebApi.Services;
using PadraoWebApi.VOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PadraoWebApi.XML
{
    public class WebServiceReturnService : IWebServiceReturn
    {
        public List<WebServiceRegisterVO> Registers { get; set; }
        WebServiceFieldVO registerField;
        WebServiceRegisterVO register;

        public WebServiceReturnService()
        {
            Registers = new List<WebServiceRegisterVO>();
        }

        public void ProcessWebServiceReturn(String xmlRetorno)
        {
            //Messages = new List<NotificationMessage>();

            WebServiceReturn webServiceReturn = new WebServiceReturn();

            if (!String.IsNullOrEmpty(xmlRetorno))
            {
                XMLService.ReadXML(webServiceReturn, xmlRetorno);
            }

            foreach (WebServiceReturn.ErroMSGRow msg in webServiceReturn.ErroMSG)
            {

                Debug.Print("Mensagem de erro >>>> " + msg.ermDSErro);
                //Messages.Add(
                //    new NotificationMessage(String.Format("{0} {1}", msg.ermDSErro, msg.ermMensagemAux),
                //        msg.ermErro == 0 ? NotificationType.Message : NotificationType.Error) { Code = msg.ermCDErro });

            }


            foreach (WebServiceReturn.RegistroRow registroRow in webServiceReturn.Registro)
            {
                register = new WebServiceRegisterVO();

                register.Sequence = registroRow.wsrrSequencial;
                register.Type = registroRow.wsrrTipo;


                var listaCampo = from campos in webServiceReturn.Campos
                                 join campo in webServiceReturn.Campo
                                  on campos.Campos_Id equals campo.Campos_Id
                                 where campos.Registro_Id == registroRow.Registro_Id
                                 select campo;

                foreach (WebServiceReturn.CampoRow campoRow in listaCampo)
                {
                    registerField = new WebServiceFieldVO();
                    registerField.Name = campoRow.wsrcNome;
                    registerField.Value = campoRow.wsrcValor;
                    register.Fields.Add(registerField);
                }

                Registers.Add(register);

                //MXMLogger.LogFileSQL();
            }
        }
    }
}
