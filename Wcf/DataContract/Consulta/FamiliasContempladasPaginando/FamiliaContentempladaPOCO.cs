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

using System;
using System.Runtime.Serialization;

namespace Wcf.DataContract.Consulta.FamiliasContempladasPaginando
{
    [DataContract]
    public class FamiliaContentempladaPOCO
    {
        [DataMember]
        public int FamiliaID { get; set; }

        [DataMember]
        public int QuantiadeCriteriosAtendidos { get; set; }

        [DataMember]
        public int PontuacaoTotal { get; set; }

        [DataMember]
        public DateTime DataFamiliaFoiSelecionada { get; set; }
    }
}