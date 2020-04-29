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
    public class RendaBusiness : BaseBusiness<Renda>
    {
        private readonly IRendaRepository repository;

        public RendaBusiness()
        {
            repository = new RendaRepository();
        }

        public IQueryable<Renda> ObterTodos()
        {
            return repository.ObterTodos();
        }

        public Renda Obter(int id)
        {
            return repository.Obter(x => x.RendaID == id);
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
                AdicionarMensagem("Renda não encontrada!", ETipoMensagem.ERRO);
                EhValido = false;
                return;
            } 
            if (repository.Remover(Entidade))
                AdicionarMensagem("Renda removida com sucesso!", ETipoMensagem.SUCESSO);
            else
            {
                AdicionarMensagem("Não foi possível remover a Renda!", ETipoMensagem.SUCESSO);
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
