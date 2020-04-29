/*************************************************************
 * Desafio Casa Popular
 *************************************************************
 * Criado por: Denny Sakakibara
 * Data da criação: 26/04/2020
 * Modificado por: 
 * Data da modificação: 
 * Observação: 
 * ***********************************************************
 */

using Business.Cadastro;
using Core.Logic.ConstantTypes;
using System;
using System.Linq;
using Wcf.DataContract.Consulta.FamiliasContempladasPaginando;

namespace Wcf.Consulta
{
    public class Familia : IFamilia
    {
        private FamiliaBusiness _familiaBusiness;
        private FamiliaBusiness familiaBusiness => _familiaBusiness ?? (_familiaBusiness = new FamiliaBusiness());

        public ConsultaFamiliasContempladasPaginando ConsultarFamiliasContempladasPaginando(int PaginaAtual, ELimite limitePorPagina)
        {
            var retorno = new ConsultaFamiliasContempladasPaginando();

            try
            {
                familiaBusiness.PaginaAtual = PaginaAtual;
                familiaBusiness.LimitePorPagina = (int)limitePorPagina;
                familiaBusiness.ConsultarPaginacaoFamiliasContempladas();

                retorno.PaginaAtual = familiaBusiness.PaginaAtual;
                retorno.QtdItens = familiaBusiness.QtdtItens;
                retorno.QtdPaginas = familiaBusiness.QtdPaginas;

                if (retorno.QtdItens > 0)
                {
                    retorno.Familias = familiaBusiness.Familias.Select(x => new FamiliaContentempladaPOCO()
                    {
                        FamiliaID = x.FamiliaID,
                        DataFamiliaFoiSelecionada = x.PontuacaoFamilia.DataFamiliaSelecionada.Value,
                        PontuacaoTotal = x.PontuacaoFamilia.TotalPontos,
                        QuantiadeCriteriosAtendidos = x.PontuacaoFamilia.CriteriosAtendidos.Count()
                    }).ToList();
                }
            }
            catch (Exception)
            {
                retorno.EhValido = false;
                retorno.Mensagem = "Erro ao consultar Famílias Contempladas! Entre em contato com o responsável pelo webservice através do email contato@email.com";
            }

            return retorno;
        }
    }
}