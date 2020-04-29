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

namespace Core.Logic.Cadastro
{
    public class Pessoa
    {
        public Pessoa()
        {
            Rendas = new List<Renda>();
        }

        public int PessoaID { get; set; }
        public string Nome { get; set; }
        public ETipoPessoaFamilia Tipo { get; set; }
        public DateTime DataDeNascimento { get; set; }

        public int FamiliaID { get; set; }
        public Familia Familia { get; set; }

        public virtual ICollection<Renda> Rendas { get; set; }
    }
}
