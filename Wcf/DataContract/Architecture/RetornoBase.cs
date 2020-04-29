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

using System.Runtime.Serialization;

namespace Wcf.DataContract.Architecture
{
    [DataContract]
    public class RetornoBase
    {
        public RetornoBase()
        {
            EhValido = true;
        }

        [DataMember]
        public bool EhValido { get; set; }

        [DataMember]
        public string Mensagem { get; set; }
    }
}