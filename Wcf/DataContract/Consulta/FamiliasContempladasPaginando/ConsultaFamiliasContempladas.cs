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

using System.Collections.Generic;
using System.Runtime.Serialization;
using Wcf.DataContract.Architecture;

namespace Wcf.DataContract.Consulta.FamiliasContempladasPaginando
{
    [DataContract]
    public class ConsultaFamiliasContempladasPaginando : RetornoBase
    {
        [DataMember]
        public int PaginaAtual { get; set; }
         
        [DataMember]
        public int QtdItens { get; set; }
         
        [DataMember]
        public int QtdPaginas { get; set; }

        [DataMember]
        public List<FamiliaContentempladaPOCO> Familias { get; set; }
    }
}