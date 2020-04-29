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

using Core.Logic.ConstantTypes;
using System.ServiceModel;
using Wcf.DataContract.Consulta.FamiliasContempladasPaginando;

namespace Wcf.Consulta
{ 
    [ServiceContract]
    public interface IFamilia
    {
        [OperationContract]
        ConsultaFamiliasContempladasPaginando ConsultarFamiliasContempladasPaginando(int PaginaAtual, ELimite limitePorPagina);
    }
}