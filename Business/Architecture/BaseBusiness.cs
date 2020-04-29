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
 
using Commons.Util;
using Core.Logic.ConstantTypes;
using System.Collections.Generic;
using System.Linq;

namespace Business.Architecture
{
    public abstract class BaseBusiness<E> where E : class
    {
        private readonly List<Mensagem> mensagensBusiness;

        public E Entidade { get; set; }

        public abstract void ConsultarPaginacao();
        protected abstract void PrepararParametrosDaConsulta();
        protected abstract int ContarPaginacao();

        public int QtdtItens { get; set; }
        public int QtdPaginas { get; set; }
        public int PaginaAtual { get; set; }
        public int LimitePorPagina { get; set; }

        public bool EhValido { get; set; }

        protected abstract bool EstadoValido();

        protected BaseBusiness()
        {
            mensagensBusiness = new List<Mensagem>();
            EhValido = true;
        }

        protected void AdicionarMensagem(Mensagem mensagem)
        {
            mensagensBusiness.Add(mensagem);
        }

        protected void LimparMensagem()
        {
            mensagensBusiness.Clear();
        }

        protected void LimparMensagem(ETipoMensagem tipoMensagem)
        {
            mensagensBusiness.RemoveAll(m => m.Tipo == tipoMensagem);
        }

        protected void AdicionarMensagem(string mensagem, ETipoMensagem mensagemTipo)
        {
            if (mensagemTipo == ETipoMensagem.ERRO)
                mensagem = "Não foi possível realizar a operação! " + mensagem;

            mensagensBusiness.Add(new Mensagem(mensagem, mensagemTipo));
        }

        protected void AdicionarMensagens(List<Mensagem> mensagens)
        {
            foreach (var msg in mensagens)
                mensagensBusiness.Add(msg);
        }

        public List<Mensagem> ObterMensagens()
        {
            return mensagensBusiness;
        }

        public int ContarMensagens()
        {
            return mensagensBusiness.Count;
        }

        protected bool ContemMensagensDoTipo(ETipoMensagem tipoMensagem)
        {
            return mensagensBusiness.Any(x => x.Tipo == tipoMensagem);
        } 
    }
} 