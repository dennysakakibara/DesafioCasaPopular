/*************************************************************
 * Desafio Casa Popular
 *************************************************************
 * Criado por: Denny Sakakibara
 * Data da criação: 25/04/2020
 * Modificado por: 
 * Data da modificação: 
 * Observação: 
 * ***********************************************************
 */

using Business.Architecture;
using Core.Logic.Cadastro;
using Core.Logic.ConstantTypes;
using Core.Logic.PontuacaoCasaPopular;
using Repository.Modules.Cadastro;
using Repository.Modules.Cadastro.Interface;
using System;
using System.Linq;

namespace Business.PontuacaoCasaPopular
{
    public class CriterioPontuacaoBusiness : BaseBusiness<CriterioPontuacao>
    {
        private ICriterioPontuacaoRepository _repository;
        private ICriterioPontuacaoRepository repository => _repository ?? (_repository = new CriterioPontuacaoRepository());

        public CriterioPontuacao Obter(int id)
        {
            return repository.Obter(x => x.CriterioPontuacaoID == id);
        }

        public void Inserir(int id)
        {
            throw new NotImplementedException();
        }

        public void Inativar(int id)
        {
            throw new NotImplementedException();
        }

        public void Ativar(int id)
        {
            throw new NotImplementedException();
        }

        protected override bool EstadoValido()
        {
            throw new NotImplementedException();
        }

        public IQueryable<CriterioPontuacao> ObterTodos()
        {
            return repository.ObterTodos();
        }

        public IQueryable<CriterioPontuacao> ObterTodosAtivos()
        {
            return ObterTodos().Where(x => x.SituacaoID == ESituacao.Ativo);
        }
         
        public override void ConsultarPaginacao()
        {
            throw new NotImplementedException();
        }

        protected override void PrepararParametrosDaConsulta()
        {
            throw new NotImplementedException();
        }

        protected override int ContarPaginacao()
        {
            throw new NotImplementedException();
        }
         
        public bool AtendeCriterio(Familia familia, CriterioPontuacao criterio)
        {
            switch ((ECriterioPontuacao)criterio.CriterioPontuacaoID)
            {
                case ECriterioPontuacao.Renda_total_da_família_até_900_reais:
                    { // utilizando chaves para poder usar mesmo nome de variavel nos demais casos, para sempre seguir uma mesma linha de raciocínio.
                        var rendaTotal = familia.ObterRendas().Sum(r => r.Valor);
                        if (rendaTotal <= 900)
                            return true;
                    }
                    break;

                case ECriterioPontuacao.Renda_total_da_família_de_901_à_1500_reais:
                    {
                        var rendaTotal = familia.ObterRendas().Sum(r => r.Valor);
                        if (901 <= rendaTotal && rendaTotal <= 1500)
                            return true;
                    }
                    break;

                case ECriterioPontuacao.Renda_total_da_família_de_1501_à_2000_reais:
                    {
                        var rendaTotal = familia.ObterRendas().Sum(r => r.Valor);
                        if (1501 <= rendaTotal && rendaTotal <= 2000)
                            return true;
                    }
                    break;

                case ECriterioPontuacao.Pretendente_com_idade_igual_ou_acima_de_45_anos:
                    {
                        var dataLimite = DateTime.Now.AddYears(-45);
                        var totalPessoas = familia.Pessoas.Where(p => p.Tipo == ETipoPessoaFamilia.Pretendente &&
                                                                      p.DataDeNascimento <= dataLimite).Count();
                        if (totalPessoas > 0)
                            return true;
                    }
                    break;

                case ECriterioPontuacao.Pretendente_com_idade_de_30_à_44_anos:
                    {
                        var dataLimiteMenor = DateTime.Now.AddYears(-30);
                        var dataLimiteMaior = DateTime.Now.AddYears(-44);

                        var totalPessoas = familia.Pessoas.Where(p => p.Tipo == ETipoPessoaFamilia.Pretendente &&
                                                                      dataLimiteMenor <= p.DataDeNascimento &&
                                                                      p.DataDeNascimento <= dataLimiteMaior).Count();
                        if (totalPessoas > 0)
                            return true;
                    }
                    break;

                case ECriterioPontuacao.Pretendente_com_idade_abaixo_de_30_anos:
                    {
                        var dataLimite = DateTime.Now.AddYears(-30);
                        var totalPessoas = familia.Pessoas.Where(p => p.Tipo == ETipoPessoaFamilia.Pretendente &&
                                                                      dataLimite <= p.DataDeNascimento).Count();
                        if (totalPessoas > 0)
                            return true;
                    }
                    break;

                case ECriterioPontuacao.Famílias_com_3_ou_mais_dependentes_lembrando_que_dependentes_maiores_de_18_anos_não_contam:
                    {
                        var dataLimite = DateTime.Now.AddYears(-18);
                        var totalPessoas = familia.Pessoas.Where(p => p.Tipo == ETipoPessoaFamilia.Dependente &&
                                                                      dataLimite <= p.DataDeNascimento).Count();
                        if (totalPessoas >= 3)
                            return true;
                    }
                    break;

                case ECriterioPontuacao.Famílias_com_1_ou_2_dependentes__lembrando_que_dependentes_maiores_de_18_anos_não_contam:
                    {
                        var dataLimite = DateTime.Now.AddYears(-18);
                        var totalPessoas = familia.Pessoas.Where(p => p.Tipo == ETipoPessoaFamilia.Dependente &&
                                                                      dataLimite <= p.DataDeNascimento).Count();
                        if (totalPessoas == 1 || totalPessoas == 2)
                            return true;
                    }
                    break;

                // caso apareçam novos criterios, eles podem ser adicionados aqui.
                // o criterio foi implementado como tabela para facilitar a ativação ou a inativação de um determinado criterio sem precisar esperar
                // uma publicacao futura de sistema para efetuar a alteração.
                // é possível criar uma funcionalidade de controle para dar manutençao dos criterios ativos e inativos.

                // foi criado esse enum de criterio possuindo o mesmo ID do criterio do banco. Facillitando a localizacao de sua implementacao 

                default:
                    break;
            }

            return false;
        }
    }
}