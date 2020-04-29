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
  
using Repository.Logic.Mapping.Cadastro;
using Repository.Logic.Mapping.PontuacaoCasaPopular;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Repository.Logic.Context
{
    public class DesafioCasaPopularContext : DbContext
    {
        //public DbSet<TABELA> dbsTABELA { get; set; }

        static DesafioCasaPopularContext()
        {
            Database.SetInitializer<DesafioCasaPopularContext>(null);
        }

        public DesafioCasaPopularContext() : base("name=ApplicationEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
  
            //////////////
            // Cadastro // 
            //////////////

            //modelBuilder.Configurations.Add(new FamiliaMap());
            //modelBuilder.Configurations.Add(new PessoaMap());
            //modelBuilder.Configurations.Add(new RendaMap());

            //////////////////////////
            // PontuacaoCasaPopular // 
            //////////////////////////
          //  modelBuilder.Configurations.Add(new CriterioPontuacaoMap()); 
        }
    }
}