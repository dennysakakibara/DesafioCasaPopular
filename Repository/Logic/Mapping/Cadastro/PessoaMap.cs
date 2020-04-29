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

using Core.Logic.Cadastro;
using System.Data.Entity.ModelConfiguration;

namespace Repository.Logic.Mapping.Cadastro
{
    public class PessoaMap : EntityTypeConfiguration<Pessoa>
    {
        public PessoaMap()
        {
            // Primary Key
            HasKey(t => t.PessoaID);

            // Properties
            Property(t => t.PessoaID).IsRequired();
            Property(t => t.Nome).IsRequired().HasMaxLength(400);
            Property(t => t.Tipo).IsRequired();
            Property(t => t.DataDeNascimento).IsRequired();
            Property(t => t.FamiliaID).IsRequired();

            // Table & Column Mappings
            ToTable("Pessoa");
            Property(t => t.PessoaID).HasColumnName("PessoaID");
            Property(t => t.Nome).HasColumnName("Nome");
            Property(t => t.Tipo).HasColumnName("Tipo");
            Property(t => t.DataDeNascimento).HasColumnName("DataDeNascimento");
            Property(t => t.FamiliaID).HasColumnName("FamiliaID");
        }
    }
}