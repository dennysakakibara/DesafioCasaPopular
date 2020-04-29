/*************************************************************
 * Desafio Casa Popular
 *************************************************************
 * Criado por: Denny Sakakibara
 * Data da criação: 27/04/2020
 * Modificado por: 
 * Data da modificação: 
 * Observação: 
 * ***********************************************************
 */

using Business.PontuacaoCasaPopular;
using Core.Logic.Cadastro;
using Core.Logic.ConstantTypes;
using Core.Logic.PontuacaoCasaPopular;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Business.Tests.Cadastro
{
    [TestFixture]
    public class CriterioPontuacaoBusinessTests
    {
        [Test]
        public void AtendeCriterio_Renda_total_da_família_até_900_reais_RetornoFalso()
        {
            /////////////////////////////////////////////////////////////
            //Arrange 
            /////////////////////////////////////////////////////////////
            // cria o cenario
            CriterioPontuacaoBusiness criterioPontuacaoBusiness = new CriterioPontuacaoBusiness();
            bool resultadoEsperado = false;

            Familia familia = new Familia();
            familia.FamiliaID = 1234;
             
            // pai
            var pretendentePai = new Pessoa() { PessoaID = 1, Nome = "João", Tipo = ETipoPessoaFamilia.Pretendente, DataDeNascimento = new DateTime(1989, 12, 30) };
            pretendentePai.Rendas.Add(new Renda() { RendaID = 1, PessoaID = 1, Familia = familia, Valor = 1000 });
            familia.Pessoas.Add(pretendentePai);

            // mae
            var pretendenteMAe = new Pessoa() { PessoaID = 2, Nome = "Maria", Tipo = ETipoPessoaFamilia.Cônjuge, DataDeNascimento = new DateTime(1989, 11, 30) }; 
            pretendenteMAe.Rendas.Add(new Renda() { RendaID = 2, PessoaID = 2, Familia = familia, Valor = 950 });
            familia.Pessoas.Add(pretendenteMAe);

            // dependentes
            familia.Pessoas.Add(new Pessoa() { PessoaID = 3, Nome = "José", Tipo = ETipoPessoaFamilia.Dependente, DataDeNascimento = new DateTime(2015, 06, 07) });
            familia.Pessoas.Add(new Pessoa() { PessoaID = 4, Nome = "Angela", Tipo = ETipoPessoaFamilia.Dependente, DataDeNascimento = new DateTime(2015, 01, 02) });


            CriterioPontuacao criterio = new CriterioPontuacao();
            criterio.CriterioPontuacaoID = (int)ECriterioPontuacao.Renda_total_da_família_até_900_reais;
            criterio.DataCadastro = DateTime.Now;
            criterio.Descricao = "AtendeCriterio_COM_Renda_total_da_família_até_900_reais";
            criterio.Pontuacao = 500;

            /////////////////////////////////////////////////////////////
            // Act
            /////////////////////////////////////////////////////////////
            bool resultadoMetodo = criterioPontuacaoBusiness.AtendeCriterio(familia, criterio);


            /////////////////////////////////////////////////////////////
            // Assert
            /////////////////////////////////////////////////////////////
            Assert.AreEqual(resultadoEsperado, resultadoMetodo);
        }

        [Test]
        public void AtendeCriterio_Famílias_com_1_ou_2_dependentes__lembrando_que_dependentes_maiores_de_18_anos_não_contam_RetornoVerdadeiro()
        {
            /////////////////////////////////////////////////////////////
            //Arrange 
            /////////////////////////////////////////////////////////////
            // cria o cenario
            CriterioPontuacaoBusiness criterioPontuacaoBusiness = new CriterioPontuacaoBusiness();
            bool resultadoEsperado = true;

            Familia familia = new Familia();
            familia.FamiliaID = 1234;

            familia.Pessoas = new List<Pessoa>();

            // pai
            var pretendentePai = new Pessoa() { PessoaID = 1, Nome = "João", Tipo = ETipoPessoaFamilia.Pretendente, DataDeNascimento = new DateTime(1989, 12, 30) };
            pretendentePai.Rendas = new List<Renda>();
            pretendentePai.Rendas.Add(new Renda() { RendaID = 1, PessoaID = 1, Familia = familia, Valor = 1000 });
            familia.Pessoas.Add(pretendentePai);

            // mae
            var pretendenteMAe = new Pessoa() { PessoaID = 2, Nome = "Maria", Tipo = ETipoPessoaFamilia.Cônjuge, DataDeNascimento = new DateTime(1989, 11, 30) };
            pretendenteMAe.Rendas = new List<Renda>();
            pretendenteMAe.Rendas.Add(new Renda() { RendaID = 2, PessoaID = 2, Familia = familia, Valor = 950 });
            familia.Pessoas.Add(pretendenteMAe);

            // dependentes
            familia.Pessoas.Add(new Pessoa() { PessoaID = 3, Nome = "José", Tipo = ETipoPessoaFamilia.Dependente, DataDeNascimento = new DateTime(2015, 06, 07) });
            familia.Pessoas.Add(new Pessoa() { PessoaID = 4, Nome = "Angela", Tipo = ETipoPessoaFamilia.Dependente, DataDeNascimento = new DateTime(2015, 01, 02) });


            CriterioPontuacao criterio = new CriterioPontuacao();
            criterio.CriterioPontuacaoID = (int)ECriterioPontuacao.Famílias_com_1_ou_2_dependentes__lembrando_que_dependentes_maiores_de_18_anos_não_contam;
            criterio.DataCadastro = DateTime.Now;
            criterio.Descricao = "Famílias_com_1_ou_2_dependentes__lembrando_que_dependentes_maiores_de_18_anos_não_contam";
            criterio.Pontuacao = 500;

            /////////////////////////////////////////////////////////////
            // Act
            /////////////////////////////////////////////////////////////
            bool resultadoMetodo = criterioPontuacaoBusiness.AtendeCriterio(familia, criterio);


            /////////////////////////////////////////////////////////////
            // Assert
            /////////////////////////////////////////////////////////////
            Assert.AreEqual(resultadoEsperado, resultadoMetodo);
        }
    }

}
