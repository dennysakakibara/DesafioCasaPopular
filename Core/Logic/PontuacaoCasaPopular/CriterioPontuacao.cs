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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Logic.PontuacaoCasaPopular
{
    public class CriterioPontuacao
    {
        public int CriterioPontuacaoID { get; set; }
        public string Descricao { get; set; }
        public int Pontuacao { get; set; }

        public DateTime DataCadastro { get; set; }
        public ESituacao SituacaoID { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}
