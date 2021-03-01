//using MXM.Framework.Core.Configuration;
//using MXM.Framework.Core.ContextManager;
//using MXM.Framework.Core.Data;
//using MXM.Framework.Core.DependencyInjection;
//using MXM.Framework.Security.Fix.Repository;
//using MXM.Framework.Web.Context;
//using MXM.Framework.Web.VO;
//using MXM.Framework.Web.WebService;
//using System;
//using System.Linq;

//namespace PadraoWebApi.Services
//{
//    public static class WebServiceConfigurationService
//    {

//        public static class APIConfigurationService
//        {
//            public static String GetURL(String complement, String grupo)
//            {

//                var webServicesURL = MXMConfigIndividual.GetWebServicesURL(grupo);

//                var url = String.Empty;

//                if (!String.IsNullOrWhiteSpace(webServicesURL)) //Pega a URL definida no mxm.config
//                {
//                    if (!webServicesURL.EndsWith("/"))
//                    {
//                        webServicesURL = String.Concat(webServicesURL, "/");
//                    }
//                    url = String.Format("{0}{1}", webServicesURL, "api");
//                }
//                else
//                {
//                    //Se não tem URL explícita no arquivo de configuração nem no mxm.config, tenta descobrir a URL (WebApplication.Path)
//                    //Isso não funciona em web farms onde o servidor de aplicação não enxerga o servidor do farm!!!
//                    url = String.Format("{0}{1}", WebApplicationInformation.GetURL(grupo), "api");
//                }

//                return String.Format("{0}/{1}", url, complement);
//            }
//        }

//        private static WebServiceConfigurationItemVO GetconfigurationFor(String webServiceIdentifier)
//        {
//            var context = new WebServiceConfigurationContext();
//            return context.WebServices.FirstOrDefault(f => f.Id == webServiceIdentifier);
//        }
//        public static String GetURLFor(String webServiceIdentifier, Int32? functionId = null)
//        {
//            return GetURLFor(
//                webServiceIdentifier,
//                DependencyInjectionFactory.Create<IContextManager>().CurrentDataBaseContext.DefaultDataBaseEnvironment.GroupName,
//                functionId
//                );
//        }
//        public static String GetURLFor(String webServiceIdentifier, String grupo, Int32? functionId = null)
//        {
//            var configurationReader = DependencyInjectionFactory.Create<IConfigurationReadingService>();

//            var webServicesFolder = ConfiguracoesFixasDoSistema.DiretorioWebServices;
//            var webServicesURL = MXMConfigIndividual.GetWebServicesURL(grupo);
            
//            var configuration = GetconfigurationFor(webServiceIdentifier);
//            if (configuration == null)
//            {
//                throw new Exception(String.Format("WebService {0} não encontrado no arquivo de configuração", webServiceIdentifier));
//            }

//            var url = String.Empty;

//            if (configuration.UseExplicitURL)
//            {
//                //Pega a URL definida explicitamente no arquivo de configuração
//                url = configuration.URL;
//            }
//            else
//            {
//                var configuredUrl = configuration.URL;

//                if(functionId != null)
//                {
//                    var DemandaDoFix = RepositoryFactory.CreateUsingCurrentSession<IFixedFunctionRepository>().ObterFixDeExecutavelDeWebservice(functionId.Value);
//                    if(!String.IsNullOrWhiteSpace(DemandaDoFix))
//                    {
//                        configuredUrl = String.Join("_", DemandaDoFix, configuredUrl);
//                    }
//                }


//                if (!String.IsNullOrWhiteSpace(webServicesURL)) //Pega a URL definida no mxm.config
//                {
//                    if (!webServicesURL.EndsWith("/"))
//                    {
//                        webServicesURL = String.Concat(webServicesURL, "/");
//                    }
//                    url = String.Format("{0}{1}/{2}", webServicesURL, webServicesFolder, configuredUrl);
//                }
//                else
//                {
//                    //Se não tem URL explícita no arquivo de configuração nem no mxm.config, tenta descobrir a URL (WebApplication.Path)
//                    //Isso não funciona em web farms onde o servidor de aplicação não enxerga o servidor do farm!!!
//                    url = String.Format("{0}{1}/{2}", WebApplicationInformation.GetURL(grupo), webServicesFolder, configuredUrl);
//                }
//            }

//            return url;
//        }
//    }
//}
