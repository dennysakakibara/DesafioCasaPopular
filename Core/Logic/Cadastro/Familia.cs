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
using Core.Logic.PontuacaoCasaPopular;
using System.Collections.Generic;
using System.Linq;

namespace Core.Logic.Cadastro
{
    public class Familia
    {
        public Familia()
        {
            Pessoas = new List<Pessoa>();
        }

        public int FamiliaID { get; set; }
        public EStatusFamilia StatusFamiliaID { get; set; } 

        public virtual ICollection<Pessoa> Pessoas { get; set; }

        public int? PontuacaoFamiliaID { get; set; }
        public virtual PontuacaoFamilia PontuacaoFamilia { get; set; }

        public List<Renda> ObterRendas()
        {
            if (Pessoas == null || !Pessoas.Any())
                return new List<Renda>();

            return Pessoas.SelectMany(x => x.Rendas).ToList();
        }
    }
} 