using Core.Logic.ConstantTypes;
using System;

namespace Commons.Util
{
    [Serializable]
    public class Mensagem
    {

        public Mensagem(string mensagem, ETipoMensagem tipo)
        {
            Texto = mensagem;
            Tipo = tipo;
        }

        public override string ToString()
        {
            return Texto;
        }

        public string Texto { get; set; }
        public ETipoMensagem Tipo { get; set; }
    }
}
