using AutoMapper;
using Data.Handlers;
using Domain.DTOs;
using Domain.Entidades;
using Domain.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ShoopingService : IShoppingService
    {
        private IRepository<Requisicao> _repository;
        private IRepository<ItemRequisicao> _itrepository;
        private IRepository<AnexoRequisicao> _anexorepository;

        private readonly IMapper _mapper;

        public ShoopingService(IRepository<Requisicao> repository, IRepository<ItemRequisicao> itrepository, IRepository<AnexoRequisicao> anexorepository, IMapper mapper)
        {
            _repository = repository;
            _itrepository = itrepository;
            _anexorepository = anexorepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RequisicaoDTO>> GetAllRequisicao(string usuarioName)
        {
            try
            {
                var query = queryRequisicao(usuarioName);
                var requisicoes = _repository.ExecuteQuerySelect(query);
                //var ListEntity = await _repository.SelectAsync();
                return _mapper.Map<IEnumerable<RequisicaoDTO>>(requisicoes);
            }
            catch (Exception ex)
            {
                throw tratarExcecao(ex);
            }
        }

        public async Task<IEnumerable<ItemRequisicaoDTO>> GetItemdaRequisicao(string Requisicao)
        {
            try
            {
                var requisicoes = _itrepository.QuerySelect().Where(x => x.ReqNumero == Requisicao).ToList();
                return _mapper.Map<IEnumerable<ItemRequisicaoDTO>>(requisicoes);
            }
            catch (Exception ex)
            {
                throw tratarExcecao(ex);
            }
        }

        public async Task<IEnumerable<AnexoRequisicaoDTO>> GetAnexodaRequisicao(string Requisicao)
        {
            try
            {
                var requisicoes = _anexorepository.QuerySelect().Where(x => x.ReqNumero == Requisicao).ToList();
                return _mapper.Map<IEnumerable<AnexoRequisicaoDTO>>(requisicoes);
            }
            catch (Exception ex)
            {
                throw tratarExcecao(ex);
            }
        }

        public async Task<IEnumerable<RequisicaoDTO>> GetRequisicaoporCodigo(string Requisicao)
        {
            try
            {
                var ListEntity = await _repository.SelectAsync();
                //var requisicoes = ListEntity.Where(x => x.ReqNumero == Requisicao);
                var requisicoes = _repository.QuerySelect().Where(x => x.ReqNumero == Requisicao);
                return _mapper.Map<IEnumerable<RequisicaoDTO>>(requisicoes);
            }
            catch (Exception ex)
            {
                throw tratarExcecao(ex);
            }
        }


        private static Exception tratarExcecao(Exception ex)
        {
            if (ex.InnerException != null && ((Oracle.ManagedDataAccess.Client.OracleException)ex.InnerException).Message.Contains("ORA-01017"))
            {
                return new HttpStatusException(HttpStatusCode.BadRequest, "Usuario ou senha inválido");
            }
            if (ex.InnerException != null && ((Oracle.ManagedDataAccess.Client.OracleException)ex.InnerException).Message.Contains("ORA-00942"))
            {
                string nomeTabela = "Usada pelo recurso";
                if (ex.Message != null && ex.Message.Contains("from"))
                {
                    nomeTabela = ex.Message.Split("from ")[1].Split(" ")[0];
                }
                return new HttpStatusException(HttpStatusCode.BadRequest, "Verifique se o usuário tem acesso a View/Tabela " + nomeTabela);
            }

            return new HttpStatusException(HttpStatusCode.InternalServerError, "Ocorreu um erro interno: " + ex.Message);
        }

        private string queryRequisicao(string usuarioNome)
        {
            var query = $@"
            SELECT DISTINCT RCO.rco_numero            REQNUMERO,
                RCO.rco_data              REQDATA,
                RCO.rco_empresa           EMPRESA,
                EMP.EMP_NOME              EMPRESA_NOME,
                RCO.rco_moeda             MOEDA,
                MOE.MOE_DESCRICAO         DS_MOEDA,
                RCO.rco_tipo              TIPOREQ,
                TRQ.TRQ_DESCRICAO         DS_TIPOREQ,
                RCO.rco_setor             SETOR,
                RCO.rco_requisitante      REQUISITANTE,
                RCO.rco_posfunc           POSICAO_FUNCIONAL,
                ESF.ESF_DESCRICAO         DSPOSICAO_FUNCIONAL,
                RCO.rco_obs               OBSERVACAO,
                RCO.rco_dtmov             DATAMOV,
                RCO.rco_etape             ETAPE,
                RCO.rco_tpoper            TPOPER,
                RCO.rco_reqcompra         REQCOMPRA,
                RCO.rco_usuario           USUARIO,
                RCO.rco_ordem             ORDEM,
                RCO.rco_justificativa     JUSTIFICATIVA,
                RCO.rco_devolucao         DEVOLUCAO,
                RCO.rco_protocolo         PROTOCOLO,
                RCO.rco_foraprazo         FORAPRAZO,
                RCO.rco_statusaprv        STATUSAPRV,
                RCO.rco_impresso          IMPRESSO,
                RCO.rco_encerram          ENCERRAM,
                RCO.rco_destinacao        DESTINACAO,
                RCO.rco_ccusto            CCUSTO
              FROM REQCOMPRA_RCO     RCO,
                   EMPGERAL_EMP      EMP,
                   MOEDA_MOE         MOE,
                   TPREQ_TRQ         TRQ,
                   RELAPROVIREQ_RAIR RAIR,
                   ESTRFUNC_ESF      ESF
             WHERE RCO.RCO_EMPRESA = EMP.EMP_CODIGO
               AND RCO.RCO_MOEDA = MOE.MOE_CODIGO
               AND RCO.RCO_TIPO = TRQ.TRQ_CODIGO
               AND RAIR.RAIR_NUMEROREQ = RCO.RCO_NUMERO
               AND RCO.RCO_EMPRESA = ESF.ESF_CDEMPRESA
               AND RCO.RCO_POSFUNC = ESF.ESF_CODIGO
               AND RCO.RCO_NUMERO IN
                   (SELECT DISTINCT RAIR.RAIR_NUMEROREQ
                      FROM APROVACOES_APR APR,
                           LINHAAPROVITEM_LVI LVI,
                           PROCESSOAPROV_PAPR PAPR,
                           LINHAAPROVUSUARIO_LAU LAU,
                           ESTRFUNC_ESF ESF,
                           ALOCESTRFUNC_AEF AEF,
                           RELAPROVIREQ_RAIR RAIR
                     WHERE LAU.LAU_USUARIO = '{usuarioNome}'
                       AND LAU.LAU_SQAPROVACAO = APR.APR_SQAPROVACAO
                       AND LAU.LAU_SQAPROVACAO = LVI.LVI_SQAPROVACAO
                       AND LAU.LAU_SQITEMAPROVACAO = LVI.LVI_SQITEMAPROVACAO
                       AND LAU.LAU_SQITEMLINHAAPROVACAO = LVI.LVI_SQITEMLINHAAPROV
                       AND PAPR.PAPR_CDPROCESSO = APR.APR_CDPROCESSO
                       AND (ESF.ESF_CODIGO = LAU.LAU_CDESTRFUNC OR ESF.ESF_CATEGFUNC = LAU.LAU_CATFUNC)
                       AND ESF.ESF_CDEMPRESA = LAU.LAU_CDEMPRESA
                       AND ESF.ESF_CODIGO = AEF.AEF_CODIGO
                       AND ESF.ESF_CDEMPRESA = AEF.AEF_CDEMPRESA
                       AND ESF.ESF_HOMOLOGADO IS NOT NULL
                       AND AEF.AEF_HOMOLOGADO IS NOT NULL
                       AND AEF.AEF_USUARIO = LAU.LAU_USUARIO
                       AND APR.APR_STAPROVACAO = 2
                       AND RAIR.RAIR_SQAPROVACAO = APR.APR_SQAPROVACAO)";
            return query;
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
