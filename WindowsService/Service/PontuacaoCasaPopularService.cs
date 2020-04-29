/*************************************************************
 * Desafio Casa Popular
 *************************************************************
 * Criado por: Denny Sakakibara
 * Data da criação: 26/04/2020
 * Modificado por: 
 * Data da modificação: 
 * Observação: 
 * ***********************************************************
 */
 
using Business.Cadastro;
using WindowsService.Service.Interface;

namespace WindowsService.Service
{
    public class PontuacaoCasaPopularService : IPontuacaoCasaPopularService
    {
        private FamiliaBusiness _familiaBusiness;
        private FamiliaBusiness familiaBusiness => _familiaBusiness ?? (_familiaBusiness = new FamiliaBusiness());
        
        public void GerarRegistrosPontuacoesNaoReceberamCasa()
        {
            // esses inputs poderiam tambem ficar setados no arquivo de configuracoes e nao ficar fixados no codigo.

            int familiaIDInicio = familiaBusiness.ObterMenorIDNaoTemPontucaoGerada();
            familiaBusiness.GerarRegistrosPontuacoesNaoReceberamCasa(false, // calculoIntegral // nao faz processamento em todos os registros // gera por lotes 
                                                                     500, // quantidadeLote // a cada iteracao do servico, faz o processamento de 500 registros familia
                                                                     familiaIDInicio); // calculoAPartirFamiliaID // comeca o processamento a partir desse ID
        }
    }
}