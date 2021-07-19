using AutoMapper;
using Domain.DTOs;
using Domain.Entidades;
using Domain.Handlers;
using Domain.Interfaces;
using NHibernate;
using Oracle.ManagedDataAccess.Client;
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

        public async Task<IEnumerable<RequisicaoDTO>> GetAllRequisicao(string usuarioEmail)
        {
            try
            {
                var query = queryRequisicao(usuarioEmail);
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

                //var requisicoes = _anexorepository.QuerySelect().Where(x => x.ReqNumero == Requisicao).ToList();
                var requisicoes = _anexorepository.ExecuteQuerySelect($@"select 
                                    ARC_NUMERO ReqNumero,
                                    ARC_CODIGO Codigo,
                                    ARC_ANEXO Anexo,
                                    ARC_FILENAME FileName
                                from anexoreqcompra_arc
                                where arc_numero = '{Requisicao}'
                                ");
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
            if (ex.InnerException is OracleException && ((OracleException)ex.InnerException).Message.Contains("ORA-01017"))
            {
                return new HttpStatusException(HttpStatusCode.BadRequest, "Usuario ou senha inválido");
            }
            if (ex.InnerException is OracleException && ((OracleException)ex.InnerException).Message.Contains("ORA-00942"))
            {
                string nomeTabela = "Usada pelo recurso";
                if (ex.Message != null && ex.Message.Contains("from"))
                {
                    nomeTabela = ex.Message.Split("from ")[1].Split(" ")[0];
                }
                return new HttpStatusException(HttpStatusCode.BadRequest, "Verifique se o usuário tem acesso a View/Tabela " + nomeTabela);
            }
            if (ex is ADOException)
            {
                return new HttpStatusException(HttpStatusCode.InternalServerError, "Falha no processo de casting para classe model do recurso acessado. Description: " + ex.Message);
            }

            return new HttpStatusException(HttpStatusCode.InternalServerError, "Ocorreu um erro interno: " + ex.Message);
        }

        private string queryRequisicao(string usuarioEmail)
        {
            var query = $@"SELECT DISTINCT 
                                rco_numero        REQNUMERO,
                                LAU_SQAPROVACAO   SQAPROVACAO,
                                LVI_SQITEMAPROVACAO SQITEMAPROVACAO,   
                                LVI_SQITEMLINHAAPROV SQITEMLINHAAPROV,
                                USER_APROVADOR    USUARIO_APROVADOR,    
                                rco_data          REQDATA,
                                ESF.ESF_CDEMPRESA       EMPRESA,
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
                                -- APR.ESF_CODIGO    POSICAO_FUNCIONAL,
                                -- ESF.ESF_DESCRICAO DSPOSICAO_FUNCIONAL,
                                -- ESF.ESF_CATEGFUNC CAT_FUNCIONAL, --** NOVO CAMPO
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
                                TCPR_VLRTITULO    VALOR_TOTAL,
                                RCO.RCO_REQUISITANTE       COD_REQUISITANTE,
                                USO_REQ.USO_NOME           NOME_REQUISITANTE,
                                TCPR.TCPR_NOTITULO         DOCUMENTO,
                                TCPR.TCPR_CDTPCOBR         COD_TIPOCOBRANCA,
                                TPC.TCP_DSTPCOBR           TIPO_COBRANCA,
                                RCO.RCO_POSFUNC            POSICAO_FUNCIONAL,
                                ESF.ESF_DESCRICAO          DSPOSICAO_FUNCIONAL,
                                RCO.RCO_JUSTIFICATIVA      DESC_PAGAMENTO ,
                                TCPR.TCPR_CDEMPORI         COD_EMP_ORIGEM,
                                EMP_ORIGEM.EMP_NOME        NOME_EMP_ORIGEM,
                                TCPR.TCPR_DTPROG           DT_PROG_PAG,
                                TCPR.TCPR_OBS              OBSERVACAO_TITULO
                  FROM REQCOMPRA_RCO     RCO,
                       EMPGERAL_EMP      EMP,
                       MOEDA_MOE         MOE,
                       TPREQ_TRQ         TRQ,
                       ESTRFUNC_ESF      ESF,
                       TITCP_TCPR        TCPR,
                       FORNEC_FOR,
                       USUARIO_USO USO_REQ,
                       TPCOBR_TCP TPC,
                       EMPGERAL_EMP EMP_ORIGEM,
                       (

                       SELECT DISTINCT * FROM (
                       SELECT     LAU_USUARIO USER_APROVADOR,
                                   RAIR.RAIR_NUMEROREQ, 
                           LAU_SQAPROVACAO, 
                           LVI.LVI_SQITEMAPROVACAO, 
                           LVI.LVI_SQITEMLINHAAPROV, 
                           ESF.ESF_CDEMPRESA, 
                           ESF.ESF_CODIGO, 
                           ESF.ESF_CATEGFUNC
                          FROM APROVACOES_APR,
                               LINHAAPROVITEM_LVI LVI,
                               PROCESSOAPROV_PAPR,
                               LINHAAPROVUSUARIO_LAU,
                               ESTRFUNC_ESF ESF,
                               ALOCESTRFUNC_AEF,
                               RELAPROVIREQ_RAIR RAIR,
                               USUARIO_USO USO
                         WHERE LAU_USUARIO = USO_CODIGO
                           AND USO.USO_ENDEMAIL = '{usuarioEmail}'
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
                           AND RAIR.RAIR_SQAPROVACAO = APR_SQAPROVACAO
                        UNION ALL
                       SELECT DAL_USUORTOGADO USER_APROVADOR,
                                   RAIR.RAIR_NUMEROREQ, 
                           LAU_SQAPROVACAO, 
                           LVI.LVI_SQITEMAPROVACAO, 
                           LVI.LVI_SQITEMLINHAAPROV, 
                           ESF.ESF_CDEMPRESA, 
                           ESF.ESF_CODIGO, 
                           ESF.ESF_CATEGFUNC
                          FROM APROVACOES_APR,
                               LINHAAPROVITEM_LVI LVI,
                               PROCESSOAPROV_PAPR,
                               LINHAAPROVUSUARIO_LAU,
                               ESTRFUNC_ESF ESF,
                               ALOCESTRFUNC_AEF,
                               RELAPROVIREQ_RAIR RAIR,
                               USUARIO_USO USO,
                               DELAGALCADO_DAL DAL
                         WHERE USO.USO_ENDEMAIL = '{usuarioEmail}'
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
                           AND APR_STAPROVACAO = 0
                           AND RAIR.RAIR_SQAPROVACAO = APR_SQAPROVACAO
                           ----
                           AND AEF_CDEMPRESA    = DAL_EMPRESA (+) 
                           AND  AEF_CODIGO       = DAL_ESTRFUNC (+) 
                           AND DAL_USUORTOGANTE  = LAU_USUARIO  
                           AND DAL_USUORTOGADO  = USO_CODIGO  
                           AND TRUNC(DAL_PERIODODE ) <= TRUNC(SYSDATE) 
                           AND  (TRUNC(DAL_PERIODOATE + 1) >= TRUNC(SYSDATE) OR  DAL_PERIODOATE    IS NULL) 
                           AND DAL_CANCELADO      IS NULL )

                           ) APR
                 WHERE APR.ESF_CDEMPRESA = EMP.EMP_CODIGO
                   AND RCO.RCO_MOEDA = MOE.MOE_CODIGO
                   AND RCO.RCO_TIPO = TRQ.TRQ_CODIGO
                   AND RCO.RCO_EMPRESA = ESF.ESF_CDEMPRESA
                   -- AND APR.ESF_CODIGO = ESF.ESF_CODIGO
                   AND RCO.RCO_POSFUNC = ESF.ESF_CODIGO
                   AND RCO.RCO_NUMERO = TCPR_NOPEDCOMPRA (+)
                   AND TCPR.TCPR_CDFOR = FOR_CODIGO (+)
                   AND RCO_NUMERO = APR.RAIR_NUMEROREQ
                   AND RCO.RCO_REQUISITANTE = USO_REQ.USO_CODIGO 
                   AND TCPR.TCPR_CDTPCOBR = TPC.TCP_CDTPCOBR (+) 
                   AND TCPR.TCPR_CDEMPORI = EMP_ORIGEM.EMP_CODIGO (+)
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
                   CCUSTO_CC CC,
                   TITCP_TCPR TCPR
                 WHERE PGRR_CDGRUPO = GPA.GPA_CDGRUPO
                   AND PGRR.PGRR_PLFCAIXA = FCX.FCX_CODFLUX (+)
                   AND PGRR_CTAFCAIXA = FCX.FCX_CODIGO (+)
                   AND PGRR.PGRR_PLCCUSTO = CC.CC_CODCENT (+)
                   AND PGRR.PGRR_NOCCUSTO = CC.CC_CODIGO (+)
                   AND PGRR.PGRR_CDFOR = TCPR.TCPR_CDFOR
                   and PGRR.PGRR_NOTITULO = TCPR.TCPR_NOTITULO
                   and TCPR.TCPR_NOPEDCOMPRA = '{requisicao}'
                   AND PGRR.PGRR_CDFOR = '{codigoFornecedor}'
            ";
            return query;
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

    }
}
