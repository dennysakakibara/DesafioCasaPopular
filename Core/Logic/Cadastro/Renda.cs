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

 namespace Core.Logic.Cadastro
{
    public class Renda
    {
        public int RendaID { get; set; }

        public int PessoaID { get; set; }
        public Familia Familia { get; set; }

        public int Valor { get; set; }
    }
}
