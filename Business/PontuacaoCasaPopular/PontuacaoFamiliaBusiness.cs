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
using System.Collections.Generic;
using System.Linq;

namespace Business.PontuacaoCasaPopular
{
    public class PontuacaoFamiliaBusiness : BaseBusiness<PontuacaoFamilia>
    {
        private IPontuacaoFamiliaRepository _repository;
        private IPontuacaoFamiliaRepository repository => _repository ?? (_repository = new PontuacaoFamiliaRepository());
         
        private CriterioPontuacaoBusiness _criterioPontuacaoBusiness;
        private CriterioPontuacaoBusiness criterioPontuacaoBusiness => _criterioPontuacaoBusiness ?? (_criterioPontuacaoBusiness = new CriterioPontuacaoBusiness());
         
        public List<CriterioPontuacao> CriteriosFamiliaPossui;

        public override void ConsultarPaginacao()
        {
            throw new NotImplementedException();
        }

        public PontuacaoFamilia Obter(int id)
        {
            return repository.Obter(x => x.PontuacaoFamiliaID == id);
        }

        public void Remover(int id)
        {
            Entidade = Obter(id);
            if (Entidade == null)
            {
                AdicionarMensagem("Pontuação Família não encontrada!", ETipoMensagem.ERRO);
                EhValido = false;
                return;
            }

            // remove os relacionamentos de criterios para depois conseguir remover o registro de pontuacao
            var removerCriteriosIDS = Entidade.CriteriosAtendidos.Select(x => x.CriterioPontuacaoID).ToList(); 
            foreach (var c in removerCriteriosIDS)
            {
                var criterio = Entidade.CriteriosAtendidos.First(x => x.CriterioPontuacaoID == c);
                Entidade.CriteriosAtendidos.Remove(criterio);
            }
             
            if (repository.Remover(Entidade))
                AdicionarMensagem("Pontuação Família removida com sucesso!", ETipoMensagem.SUCESSO);
            else
            {
                AdicionarMensagem("Não foi possível remover a Pontuação Família!", ETipoMensagem.SUCESSO);
                EhValido = false;
            }
        }

        protected override int ContarPaginacao()
        {
            throw new NotImplementedException();
        }

        protected override bool EstadoValido()
        {
            throw new NotImplementedException();
        }

        protected override void PrepararParametrosDaConsulta()
        {
            throw new NotImplementedException();
        }
         
        public int CalcularPontuacao(Familia familia)
        {
            int pontuacaoTotal = 0;
            CriteriosFamiliaPossui = new List<CriterioPontuacao>();
            List<CriterioPontuacao> criterios = criterioPontuacaoBusiness.ObterTodosAtivos().ToList();

            foreach (var c in criterios)
            {
                if (criterioPontuacaoBusiness.AtendeCriterio(familia, c))
                {
                    CriteriosFamiliaPossui.Add(c);
                    pontuacaoTotal += c.Pontuacao;
                }
            }

            return pontuacaoTotal;
        } 
    }
}
