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
using Repository.Architecture;
using Repository.Logic.Context;
using Repository.Modules.Cadastro.Interface;

namespace Repository.Modules.Cadastro
{
    public class PontuacaoFamiliaRepository : RepositorioGenerico<PontuacaoFamilia, DesafioCasaPopularContext>, IPontuacaoFamiliaRepository
    {
    }
}