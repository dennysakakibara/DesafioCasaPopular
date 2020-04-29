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
using Core.Logic.Cadastro;
using Core.Logic.ConstantTypes;
using Repository.Modules.Cadastro;
using Repository.Modules.Cadastro.Interface;
using System;
using System.Linq;

namespace Business.Cadastro
{
    public class PessoaBusiness : BaseBusiness<Pessoa>
    {
        private readonly IPessoaRepository repository;


        private RendaBusiness _rendaBusiness;
        private RendaBusiness rendaBusiness => _rendaBusiness ?? (_rendaBusiness = new RendaBusiness());

        public PessoaBusiness()
        {
            repository = new PessoaRepository();
        }

        public IQueryable<Pessoa> ObterTodos()
        {
            return repository.ObterTodos();
        }

        public Pessoa Obter(int id)
        {
            return repository.Obter(x => x.PessoaID == id);
        }

        protected override bool EstadoValido()
        {
            throw new NotImplementedException();
        }

        public void Remover(int id)
        {
            Entidade = Obter(id);
            if (Entidade == null)
            {
                AdicionarMensagem("Pessoa não encontrada!", ETipoMensagem.ERRO);
                EhValido = false;
                return;
            }

            // remove os relacionamentos de rendas para depois conseguir remover o registro de pessoa
            var removerRendaIDS = Entidade.Rendas.Select(x => x.RendaID).ToList();
            foreach (var rendaID in removerRendaIDS)
            {
                var rendaRemover = Entidade.Rendas.First(x => x.RendaID == rendaID);
                Entidade.Rendas.Remove(rendaRemover);
                rendaBusiness.Remover(rendaID);
            }

            if (repository.Remover(Entidade))
                AdicionarMensagem("Pessoa removida com sucesso!", ETipoMensagem.SUCESSO);
            else
            {
                AdicionarMensagem("Não foi possível remover a Pessoa!", ETipoMensagem.SUCESSO);
                EhValido = false;
            }
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
    }
}
