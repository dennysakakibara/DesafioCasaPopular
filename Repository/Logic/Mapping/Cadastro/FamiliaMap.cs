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
    public class FamiliaMap : EntityTypeConfiguration<Familia>
    {
        public FamiliaMap()
        {
            // Primary Key
            HasKey(t => t.FamiliaID);

            // Properties
            Property(t => t.FamiliaID).IsRequired();
            Property(t => t.StatusFamiliaID).IsRequired();
            Property(t => t.PontuacaoFamiliaID).IsOptional();
             
            // Table & Column Mappings
            ToTable("Familia");
            Property(t => t.FamiliaID).HasColumnName("FamiliaID");
            Property(t => t.StatusFamiliaID).HasColumnName("Status");
            Property(t => t.PontuacaoFamiliaID).HasColumnName("PontuacaoFamiliaID");

        }
    }
     

}
