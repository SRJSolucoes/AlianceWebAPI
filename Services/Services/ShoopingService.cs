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
        private IRepository<DetReqPagamento> _drpRepository;
        private IRepository<AnexoRequisicao> _anexorepository;

        private readonly IMapper _mapper;

        public ShoopingService(
            IRepository<Requisicao> repository, 
            IRepository<ItemRequisicao> itrepository, 
            IRepository<DetReqPagamento> drpRepository, 
            IRepository<AnexoRequisicao> anexorepository, 
            IMapper mapper)
        {
            _repository = repository;
            _itrepository = itrepository;
            _drpRepository = drpRepository;
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

        public async Task<IEnumerable<ItemRequisicaoDTO>> GetItemdaRequisicao(string Requisicao, string usuarioNome)
        {
            try
            {
                var query = queryItemRequisicao(Requisicao, usuarioNome);
                var itensdaRequisicao = _itrepository.ExecuteQuerySelect(query);
                //var ListEntity = await _repository.SelectAsync();
                return _mapper.Map<IEnumerable<ItemRequisicaoDTO>>(itensdaRequisicao);
            }
            //try
            //{
            //    var requisicoes = _itrepository.QuerySelect().Where(x => x.ReqNumero == Requisicao).ToList();
            //    return _mapper.Map<IEnumerable<ItemRequisicaoDTO>>(requisicoes);
            //}
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

        public async Task<IEnumerable<DetReqPagamentoDTO>> GetDetReqPagamento(string Requisicao, string codigoFornecedor)
        {
            try
            {
                var query = queryDetReqPagamento(Requisicao, codigoFornecedor);
                var itensdaRequisicao = _drpRepository.ExecuteQuerySelect(query);
                //var ListEntity = await _repository.SelectAsync();
                return _mapper.Map<IEnumerable<DetReqPagamentoDTO>>(itensdaRequisicao);
            }
            //try
            //{
            //    var requisicoes = _itrepository.QuerySelect().Where(x => x.ReqNumero == Requisicao).ToList();
            //    return _mapper.Map<IEnumerable<ItemRequisicaoDTO>>(requisicoes);
            //}
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
            var query = $@"SELECT 
                                DISTINCT rco_numero        REQNUMERO,
                                LAU_SQAPROVACAO   SQAPROVACAO,
                                rco_data          REQDATA,
                                rco_empresa       EMPRESA,
                                EMP.EMP_NOME      EMPRESA_NOME,
                                TCPR.TCPR_CDFOR   FORNECEDOR,
                                FOR_NOME          FORNECEDOR_NOME,
                                CASE
                                   WHEN TCPR_NOPEDCOMPRA IS NOT NULL THEN 'PAGAMENTO'
                                   WHEN rco_reqcompra = 'S' THEN 'COMPRA'
                                   ELSE 'INTERNA'
                                END TIPO,     
                                rco_moeda         MOEDA,
                                MOE.MOE_DESCRICAO DS_MOEDA,
                                rco_tipo          TIPOREQ,
                                TRQ_DESCRICAO     DS_TIPOREQ,
                                rco_setor         SETOR,
                                rco_requisitante  REQUISITANTE,
                                rco_posfunc       POSICAO_FUNCIONAL,
                                ESF.ESF_DESCRICAO DSPOSICAO_FUNCIONAL,
                                rco_obs           OBSERVACAO,
                                rco_dtmov         DATAMOV,
                                rco_etape         ETAPE,
                                rco_tpoper        TPOPER,
                                rco_reqcompra     REQCOMPRA,
                                rco_usuario       USUARIO,
                                rco_ordem         ORDEM,
                                rco_justificativa JUSTIFICATIVA,
                                rco_devolucao     DEVOLUCAO,
                                rco_protocolo     PROTOCOLO,
                                rco_foraprazo     FORAPRAZO,
                                rco_statusaprv    STATUSAPRV,
                                rco_impresso      IMPRESSO,
                                rco_encerram      ENCERRAM,
                                rco_destinacao    DESTINACAO,
                                rco_ccusto        CCUSTO,
                                TCPR_VLRTITULO    VALOR_TOTAL
                  FROM REQCOMPRA_RCO     RCO,
                       EMPGERAL_EMP      EMP,
                       MOEDA_MOE         MOE,
                       TPREQ_TRQ         TRQ,
                       --RELAPROVIREQ_RAIR RAIR,
                       ESTRFUNC_ESF      ESF,
                       TITCP_TCPR        TCPR,
                       FORNEC_FOR,
                       (
                       SELECT DISTINCT RAIR.RAIR_NUMEROREQ, LAU_SQAPROVACAO
                          FROM APROVACOES_APR,
                               LINHAAPROVITEM_LVI,
                               PROCESSOAPROV_PAPR,
                               LINHAAPROVUSUARIO_LAU,
                               ESTRFUNC_ESF,
                               ALOCESTRFUNC_AEF,
                               RELAPROVIREQ_RAIR RAIR
                         WHERE LAU_USUARIO = '{usuarioNome}' AND
                            LAU_SQAPROVACAO = APR_SQAPROVACAO
                           AND LAU_SQAPROVACAO = LVI_SQAPROVACAO
                           AND LAU_SQITEMAPROVACAO = LVI_SQITEMAPROVACAO
                           AND LAU_SQITEMLINHAAPROVACAO = LVI_SQITEMLINHAAPROV
                           AND PAPR_CDPROCESSO = APR_CDPROCESSO
                           AND (ESF_CODIGO = LAU_CDESTRFUNC OR ESF_CATEGFUNC = LAU_CATFUNC)
                           AND ESF_CDEMPRESA = LAU_CDEMPRESA
                           AND ESF_CODIGO = AEF_CODIGO
                           AND ESF_CDEMPRESA = AEF_CDEMPRESA
                           AND ESF_HOMOLOGADO IS NOT NULL
                           AND AEF_HOMOLOGADO IS NOT NULL
                           AND AEF_USUARIO = LAU_USUARIO
                           AND APR_STAPROVACAO = 0
                           AND RAIR.RAIR_SQAPROVACAO = APR_SQAPROVACAO
                           ) APR
                 WHERE RCO.RCO_EMPRESA = EMP.EMP_CODIGO
                   AND RCO.RCO_MOEDA = MOE.MOE_CODIGO
                   AND RCO.RCO_TIPO = TRQ.TRQ_CODIGO
                   AND RCO.RCO_EMPRESA = ESF.ESF_CDEMPRESA
                   AND RCO.RCO_POSFUNC = ESF.ESF_CODIGO
                   AND RCO.RCO_NUMERO = TCPR_NOPEDCOMPRA (+)
                   AND TCPR.TCPR_CDFOR = FOR_CODIGO (+)
                   AND RCO_NUMERO = APR.RAIR_NUMEROREQ
";
            return query;
        }

        private string queryItemRequisicao(string requisicao, string usuarioNome)
        {
            var query = $@"
                 SELECT 
                  LAU_SQAPROVACAO       SQAPROVACAO, 
                  LAU_SQITEMAPROVACAO   SQITEMAPROVACAO,
                  irc_numero     REQNUMERO    ,
                  irc_numitem    NUMITEM    ,
                  irc_tpestoq    ESTOQUE    ,
                  irc_item       ITEM    ,
                  irc_descricao  DESCRICAO   ,
                  irc_grupocota  GRUPOCTA   ,
                  irc_qtdpedida  QTDPEDIDA   ,
                  irc_unidade    UNIDADE   ,
                  irc_entrega    ENTREGA   ,
                  irc_valor      VALOR   ,
                  irc_urgente    VBURGENTE   ,
                  irc_locentrega LOCALENTREGA  ,
                  irc_mapa       MAPA  ,
                  irc_qtdatendida QTDATENDIDA  ,
                  irc_dtatendida  DTATENDIDA 
                 FROM IREQCOMPRA_IRC IRC, 
                       (SELECT DISTINCT
                            RAIR.RAIR_NUMEROREQ, 
                            RAIR.RAIR_NUMITEM, 
                            LAU.LAU_SQAPROVACAO, 
                            LAU.LAU_SQITEMAPROVACAO
                          FROM APROVACOES_APR,
                               LINHAAPROVITEM_LVI,
                               PROCESSOAPROV_PAPR,
                               LINHAAPROVUSUARIO_LAU lau,
                               ESTRFUNC_ESF,
                               ALOCESTRFUNC_AEF,
                               RELAPROVIREQ_RAIR RAIR
                         WHERE LAU_USUARIO = '{usuarioNome}'
                            AND LAU_SQAPROVACAO = APR_SQAPROVACAO
                            AND LAU_SQAPROVACAO = LVI_SQAPROVACAO
                            AND LAU_SQITEMAPROVACAO = LVI_SQITEMAPROVACAO
                            AND LAU_SQITEMLINHAAPROVACAO = LVI_SQITEMLINHAAPROV
                            AND PAPR_CDPROCESSO = APR_CDPROCESSO
                            AND (ESF_CODIGO = LAU_CDESTRFUNC OR ESF_CATEGFUNC = LAU_CATFUNC)
                            AND ESF_CDEMPRESA = LAU_CDEMPRESA
                            AND ESF_CODIGO = AEF_CODIGO
                            AND ESF_CDEMPRESA = AEF_CDEMPRESA
                            AND ESF_HOMOLOGADO IS NOT NULL
                            AND AEF_HOMOLOGADO IS NOT NULL
                            AND AEF_USUARIO = LAU_USUARIO
                            AND APR_STAPROVACAO = 0
                            AND RAIR.RAIR_SQAPROVACAO = APR_SQAPROVACAO)
                WHERE irc_numero = RAIR_NUMEROREQ
                AND   irc_numitem  = RAIR_NUMITEM
                AND   irc_numero = '{requisicao}' ";
            return query;
        }

        private string queryDetReqPagamento(string requisicao, string codigoFornecedor)
        {
            var query = $@"
                 select 
                  PGRR.PGRR_CDGRUPO   GRPAG,
                  GPA.GPA_DSGRUPO     DSGRPAG,
                  PGRR.PGRR_NOCCUSTO  CCUSTO,
                  CC.CC_DESCRICAO     DSCCUSTO,
                  PGRR.PGRR_CTAFCAIXA CTAFLUXO,
                  FCX.FCX_DESCRICAO   DSCTAFLUXO,
                  PGRR.PGRR_VLRGRUPO  VLRITEM
                 from  
                   TITCPGR_PGRR PGRR,
                   GRPAG_GPA GPA,
                   FLUXOCX_FCX FCX,
                   CCUSTO_CC CC
                 WHERE PGRR_CDGRUPO = GPA.GPA_CDGRUPO
                   AND PGRR.PGRR_PLFCAIXA = FCX.FCX_CODFLUX (+)
                   AND PGRR_CTAFCAIXA = FCX.FCX_CODIGO (+)
                   AND PGRR.PGRR_PLCCUSTO = CC.CC_CODCENT (+)
                   AND PGRR.PGRR_NOCCUSTO = CC.CC_CODIGO (+)
                   AND PGRR.PGRR_NOTITULO = '{requisicao}'
                   AND PGRR.PGRR_CDFOR = '{codigoFornecedor}' ";
            return query;
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

    }
}
