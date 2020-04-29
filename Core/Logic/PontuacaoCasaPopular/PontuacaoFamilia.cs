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

using Core.Logic.ConstantTypes;
using System;
using System.Collections.Generic;

namespace Core.Logic.PontuacaoCasaPopular
{
    public class PontuacaoFamilia
    {
        public int PontuacaoFamiliaID { get; set; }
        public int TotalPontos { get; set; }
        public virtual ICollection<CriterioPontuacao> CriteriosAtendidos { get; set; }
         
        public DateTime DataCadastro { get; set; }
        public ESituacao SituacaoID { get; set; }
        public DateTime DataAtualizacao { get; set; }

        public DateTime? DataFamiliaSelecionada { get; set; }
    }
}