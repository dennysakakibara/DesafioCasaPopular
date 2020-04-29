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

using Core.Logic.PontuacaoCasaPopular;
using System.Data.Entity.ModelConfiguration;

namespace Repository.Logic.Mapping.PontuacaoCasaPopular
{
    public class CriterioPontuacaoMap : EntityTypeConfiguration<CriterioPontuacao>
    {
        public CriterioPontuacaoMap()
        {
            // Primary Key
            HasKey(t => t.CriterioPontuacaoID);

            // Properties
            Property(t => t.Descricao).IsRequired();
            Property(t => t.Pontuacao).IsRequired();
            Property(t => t.DataCadastro).IsRequired();
            Property(t => t.SituacaoID).IsRequired();
            Property(t => t.DataAtualizacao).IsOptional();


            // Table & Column Mappings
            ToTable("CriterioPontuacao");
            Property(t => t.CriterioPontuacaoID).HasColumnName("CriterioPontuacaoID");
            Property(t => t.Descricao).HasColumnName("Descricao");
            Property(t => t.Pontuacao).HasColumnName("PontuacaoFamiliaID");
            Property(t => t.DataCadastro).HasColumnName("DataCadastro");
            Property(t => t.SituacaoID).HasColumnName("SituacaoID");
            Property(t => t.DataAtualizacao).HasColumnName("DataAtualizacao"); 
        }
    } 
} 