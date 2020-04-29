/*************************************************************
 * Desafio Casa Popular
 *************************************************************
 * Criado por: Denny Sakakibara
 * Data da criação: 24/04/2020
 * Modificado por: 
 * Data da modificação: 
 * Observação: 
 * ***********************************************************
 */

using Business.Architecture;
using Business.PontuacaoCasaPopular;
using Core.Logic.Cadastro;
using Core.Logic.ConstantTypes;
using Core.Logic.PontuacaoCasaPopular;
using Repository.Modules.Cadastro;
using Repository.Modules.Cadastro.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using LinqKit;
 
namespace Business.Cadastro
{
    public class FamiliaBusiness : BaseBusiness<Familia>
    {
        private readonly IFamiliaRepository repository;

        private PontuacaoFamiliaBusiness _pontuacaoFamiliaBusiness;
        private PontuacaoFamiliaBusiness pontuacaoFamiliaBusiness => _pontuacaoFamiliaBusiness ?? (_pontuacaoFamiliaBusiness = new PontuacaoFamiliaBusiness());

        private Expression<Func<Familia, bool>> predicado;
         
        public IQueryable<Familia> Familias { get; set; }
  
        public FamiliaBusiness()
        {
            //////////////////////////////////////////////
            //repository = new FamiliaRepository();
            //////////////////////////////////////////////
            // TODO: comentei essa linha só para testar o project de testes. // verificar motivo de nao rodar no projeto de teste 
        }

        public IQueryable<Familia> ObterTodos()
        {
            return repository.ObterTodos();
        }

        public Familia Obter(int id)
        {
            return repository.Obter(x => x.FamiliaID == id);
        }

        protected override bool EstadoValido()
        {
            if (Entidade.Pessoas == null || !Entidade.Pessoas.Any())
            {
                AdicionarMensagem("Informe as pessoas da família!", ETipoMensagem.ERRO_DE_VALIDAÇÃO);
                EhValido = false;
            }

            return EhValido;
        }

        public void Salvar()
        {
            if (EstadoValido())
            {
                // se for um desses dois status, nao precisa nem alterar, pois ja foram beneficiadas
                if (Entidade.StatusFamiliaID != EStatusFamilia.Já_Possui_Uma_Casa &&
                    Entidade.StatusFamiliaID != EStatusFamilia.Selecionada_Em_Outro_Processo_De_Seleção)
                    Entidade.StatusFamiliaID = EStatusFamilia.Cadastro_Válido;
            }
            else
            {
                // se for um desses dois status, nao precisa nem alterar, pois ja foram beneficiadas
                if (Entidade.StatusFamiliaID != EStatusFamilia.Já_Possui_Uma_Casa &&
                    Entidade.StatusFamiliaID != EStatusFamilia.Selecionada_Em_Outro_Processo_De_Seleção)
                    Entidade.StatusFamiliaID = EStatusFamilia.Cadastro_Incompleto;
            }

            if (Entidade.FamiliaID == 0)
                Inserir();
            else
                Editar();
        }

        private void Inserir()
        {
            var dataAtual = DateTime.Now;
            var totalPontos = pontuacaoFamiliaBusiness.CalcularPontuacao(Entidade);
            var criteriosFamiliaPossui = pontuacaoFamiliaBusiness.CriteriosFamiliaPossui;

            var pontuacaoFamilia = new PontuacaoFamilia();
            pontuacaoFamilia.TotalPontos = totalPontos;
            pontuacaoFamilia.DataCadastro = dataAtual;
            criteriosFamiliaPossui.ForEach(x => { pontuacaoFamilia.CriteriosAtendidos.Add(x); });
            pontuacaoFamilia.DataCadastro = dataAtual;
            pontuacaoFamilia.SituacaoID = ESituacao.Ativo;
            Entidade.PontuacaoFamilia = pontuacaoFamilia;

            if (repository.Inserir(Entidade))
                AdicionarMensagem("Família cadastrada com sucesso!", ETipoMensagem.SUCESSO);
            else
            {
                AdicionarMensagem("Não foi possível cadastrar a Família!", ETipoMensagem.ERRO);
                EhValido = false;
            }
        }

        private void Editar()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                var dataAtual = DateTime.Now;
                var familiaBanco = Obter(Entidade.FamiliaID);

                EhValido = TratarEdicaoPessoasFamilia(familiaBanco, dataAtual);
                if (!EhValido)
                {
                    ts.Dispose();
                    return;
                }

                EhValido = TratarEdicaoPontuacaoFamilia(familiaBanco, dataAtual);
                if (!EhValido)
                {
                    ts.Dispose();
                    return;
                }


                EhValido = repository.Atualizar(familiaBanco);

                if (EhValido)
                {
                    AdicionarMensagem("Família atualizada com sucesso!", ETipoMensagem.SUCESSO);
                    ts.Complete();
                }
                else
                {
                    ts.Dispose();
                    AdicionarMensagem("Não foi possível atualizar a Família!", ETipoMensagem.ERRO);
                }
            }
        }

        private bool TratarEdicaoPessoasFamilia(Familia familiaBanco, DateTime dataAtual)
        {
            var pessoasIDsBanco = familiaBanco.Pessoas.Select(x => x.PessoaID).ToList();
            var pessoasIDsAlteracao = new List<int>();
            if (Entidade.Pessoas != null)
                Entidade.Pessoas.Select(x => x.PessoaID).ToList();

            // verifica quem foi removido da lista
            var pessoasIDsRemover = pessoasIDsBanco.Where(x => !pessoasIDsAlteracao.Contains(x)).ToList();

            foreach (var r in pessoasIDsRemover)
            {
                var remover = familiaBanco.Pessoas.First(x => x.PessoaID == r);
                familiaBanco.Pessoas.Remove(remover);

            }

            return true;
        }

        private bool TratarEdicaoPontuacaoFamilia(Familia familiaBanco, DateTime dataAtual)
        {

            if (Entidade.StatusFamiliaID == EStatusFamilia.Cadastro_Válido)
            {
                if (familiaBanco.PontuacaoFamiliaID.HasValue)
                {
                    // removendo os dados da pontuacao anterior
                    familiaBanco.PontuacaoFamilia = null;
                    pontuacaoFamiliaBusiness.Remover(familiaBanco.PontuacaoFamiliaID.Value);
                    EhValido = pontuacaoFamiliaBusiness.EhValido;
                    if (!EhValido)
                    {
                        AdicionarMensagens(pontuacaoFamiliaBusiness.ObterMensagens());
                        return false;
                    }
                }

                // gerando novos dados de acordo com a alteracao do cadastro
                var totalPontos = pontuacaoFamiliaBusiness.CalcularPontuacao(Entidade);
                var criteriosFamiliaPossui = pontuacaoFamiliaBusiness.CriteriosFamiliaPossui;

                var pontuacaoFamilia = new PontuacaoFamilia();
                pontuacaoFamilia.TotalPontos = totalPontos;
                pontuacaoFamilia.DataCadastro = dataAtual;
                criteriosFamiliaPossui.ForEach(x => { pontuacaoFamilia.CriteriosAtendidos.Add(x); });
                pontuacaoFamilia.DataCadastro = dataAtual;
                pontuacaoFamilia.SituacaoID = ESituacao.Ativo;
                Entidade.PontuacaoFamilia = pontuacaoFamilia;
            }

            return true;
        }

        public void RecalcularPontuacoesNaoReceberamCasa()
        {
            RemoverRegistrosPontuacaoNaoReceberamCasa();

            if (!EhValido)
                return;

            GerarRegistrosPontuacoesNaoReceberamCasa();
        }

        /// <summary>
        /// caso haja alteracao nos criterios e queria que seja recalculada a pontuacao, pode remover todos os calculos ja feitos anteriormente
        /// daqueles que nao receberam o beneficio
        /// </summary>
        /// <returns></returns> 
        private bool RemoverRegistrosPontuacaoNaoReceberamCasa()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// se o numero possiveis beneficiarios for pequeno, pode utilizar o calculo de todas as familias parametrizando calculoIntegral verdadeiro.
        /// caso a quantidade ja esteja em um numero que o processamento é bem demorado, pode ir processando parcialmente deixando
        /// que um serviço rode no servidor em um horario que o processador nao esteja tao disputado.
        /// </summary> 
        public bool GerarRegistrosPontuacoesNaoReceberamCasa(bool calculoIntegral = false, int? quantidadeLote = null, int? calculoAPartirFamiliaID = null)
        {
            throw new NotImplementedException();
        }

        public int ObterMenorIDNaoTemPontucaoGerada()
        {
            return ObterTodos().Where(x => x.StatusFamiliaID == EStatusFamilia.Cadastro_Válido &&
                                           !x.PontuacaoFamiliaID.HasValue)
                               .First()
                               .FamiliaID;
        }

        /// <summary>
        /// esse metodo retorna todas as familias aptas. se esse cadastro representar uma area muito grande,
        /// rovavelmente, essa lista ficará muito pesada e será melhor utilizar a consulta com paginacao (ConsultarPaginacao) 
        /// </summary>
        /// <returns></returns>
        public IQueryable<Familia> ObterAptasGanharCasaPopular()
        {
            return ObterTodos().Where(x => x.StatusFamiliaID == EStatusFamilia.Cadastro_Válido)
                               .OrderByDescending(x => x.PontuacaoFamilia.TotalPontos); // favorece familias melhores pontuadas           
        }

        public override void ConsultarPaginacao()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// a funcionalidade que for utilizar esse método deve informar a PaginaAtual
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// variaveis utilizadas no output da informacao
        /// viewModel.QtdItens =  QtdtItens;
        /// viewModel.QtdPaginas =  QtdPaginas;
        /// viewModel.Familias =  Familias;
        /// </summary> 
        public void ConsultarPaginacaoAptasGanharCasaPopular()
        { 
            predicado = PredicateBuilder.New<Familia>();
            predicado = c => true;
            predicado = predicado.And(f => f.StatusFamiliaID == EStatusFamilia.Cadastro_Válido);

            QtdtItens = repository.ContarUtilizandoPredicado(predicado); //QtdtItens = ContarPaginacao();

            QtdPaginas = QtdtItens > (int)ELimite.MAXIMO_POR_PAGINA ? QtdtItens / (int)ELimite.MAXIMO_POR_PAGINA : 0;

            if (QtdtItens % (int)ELimite.MAXIMO_POR_PAGINA > 0)
                QtdPaginas++;

            if (QtdtItens > 0)
            {
                int inicioPesquisa = PaginaAtual > 0 ? (PaginaAtual - 1) * (int)ELimite.MAXIMO_POR_PAGINA : 0;
                Familias = repository.ObterUtlilizandoPredicadoPaginandoDecrescente(predicado,
                                                                                    r => r.PontuacaoFamilia.TotalPontos,
                                                                                    inicioPesquisa,
                                                                                    (int)ELimite.MAXIMO_POR_PAGINA);
            }
        }

        protected override void PrepararParametrosDaConsulta()
        {
            throw new NotImplementedException();
        }

        protected override int ContarPaginacao()
        {
            throw new NotImplementedException();
        }
         
        public void AtribuirFamiliaComoSelecionada(int familiaID)
        {
            var familia = Obter(familiaID);

            if (familia.StatusFamiliaID != EStatusFamilia.Cadastro_Válido)
            {
                AdicionarMensagem("Essa família não possui um cadastro válido! Não está apta para ser selecionada!", ETipoMensagem.ERRO_DE_VALIDAÇÃO);
                EhValido = false;
                return;
            }

            if (!familia.PontuacaoFamiliaID.HasValue)
            {
                AdicionarMensagem("Essa família não possui pontuação calculada!", ETipoMensagem.ERRO_DE_VALIDAÇÃO);
                EhValido = false;
                return;
            }

            familia.PontuacaoFamilia.DataFamiliaSelecionada = DateTime.Now;

            EhValido = repository.Atualizar(familia);

            if (EhValido)
                AdicionarMensagem("Família atribuída como selecionada com sucesso!", ETipoMensagem.SUCESSO);
            else
                AdicionarMensagem("Não foi possível atribuir essa família como selecionada!", ETipoMensagem.ERRO);
        }

        /// <summary>
        /// retornando uma lista de familia, a funcionalidade que for utilizar pode montar da forma que achar necessaria.
        /// por exemplo, um WCF interno pertencente a esse projeto pode configurar os campos que acha necessario mostrar limitando partes importantes do objeto.
        /// </summary> 
        public IQueryable<Familia> ObterContempladosCasaPopular()
        {
            return ObterTodos().Where(p => p.PontuacaoFamiliaID.HasValue &&
                                           p.PontuacaoFamilia.DataFamiliaSelecionada.HasValue);

        }
         
        /// <summary>
        /// a funcionalidade que for utilizar esse método deve informar a PaginaAtual
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// variaveis utilizadas no output da informacao
        /// viewModel.QtdItens =  QtdtItens;
        /// viewModel.QtdPaginas =  QtdPaginas;
        /// viewModel.Familias =  Familias;
        /// </summary> 
        public void ConsultarPaginacaoFamiliasContempladas()
        { 
            predicado = PredicateBuilder.New<Familia>();
            predicado = c => true;
            predicado = predicado.And(p => p.PontuacaoFamiliaID.HasValue &&
                                           p.PontuacaoFamilia.DataFamiliaSelecionada.HasValue);

            QtdtItens = repository.ContarUtilizandoPredicado(predicado); //QtdtItens = ContarPaginacao();

            if (LimitePorPagina == 0)
                LimitePorPagina = (int)ELimite.MAXIMO_POR_PAGINA;
             
            QtdPaginas = QtdtItens > LimitePorPagina ? QtdtItens / LimitePorPagina : 0;

            if (QtdtItens % LimitePorPagina > 0)
                QtdPaginas++;
             
            if (QtdtItens > 0)
            {
                int inicioPesquisa = PaginaAtual > 0 ? (PaginaAtual - 1) * (int)ELimite.MAXIMO_POR_PAGINA : 0;
                Familias = repository.ObterUtlilizandoPredicadoPaginandoDecrescente(predicado,
                                                                                    r => r.FamiliaID,
                                                                                    inicioPesquisa,
                                                                                    LimitePorPagina);
            }

        }

    }
}