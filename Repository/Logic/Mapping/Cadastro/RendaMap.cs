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
    public class RendaMap : EntityTypeConfiguration<Renda>
    {
        public RendaMap()
        {
            // Primary Key
            HasKey(t => t.RendaID);

            // Properties
            Property(t => t.RendaID).IsRequired();
            Property(t => t.PessoaID).IsRequired();
            Property(t => t.Valor).IsRequired();

            // Table & Column Mappings
            ToTable("Renda");
            Property(t => t.RendaID).HasColumnName("PessoaID");
            Property(t => t.PessoaID).HasColumnName("PessoaID");
            Property(t => t.Valor).HasColumnName("Valor");
        }
    }
}