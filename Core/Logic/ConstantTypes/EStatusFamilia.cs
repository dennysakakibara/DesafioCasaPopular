/*************************************************************
 * Desafio Casa Popular
 *************************************************************
 * Criado por: Denny Sakakibara
 * Data da criação: 25/04/2020
 * Modificado por: 
 * Data da modificação: 
 * Observação: 
 * ***********************************************************
 */

namespace Core.Logic.ConstantTypes
{
    public enum EStatusFamilia
    { 
        // eu nao faria um status com identificador 0. aumenta a chance de vc erro em variaveis nao inicializadas corretamente.

        //Cadastro_Válido = 0,
        //Já_Possui_Uma_Casa = 1,
        //Selecionada_Em_Outro_Processo_De_Seleção = 2,
        //Cadastro_Incompleto = 3
             
        Cadastro_Válido = 1,
        Já_Possui_Uma_Casa = 2,
        Selecionada_Em_Outro_Processo_De_Seleção = 3,
        Cadastro_Incompleto = 4
    }
}