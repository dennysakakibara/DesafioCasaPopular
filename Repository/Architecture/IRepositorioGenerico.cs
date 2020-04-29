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

 using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repository.Architecture
{
    /// Interface para a implementação do padrão Repository usando o Entity Framework.
    public interface IRepositorioGenerico<E>
    {
        /// Inserção
        bool Inserir(E entity);
         
        /// Atualizar
        bool Atualizar(E entity);
         
        /// Exclusão
        bool Remover(E entity);

        /// Verifica se existe determinado objeto
        bool Existe(Expression<Func<E, bool>> where);

        E CriarObjeto();

        /// Retorna o objeto que satisfaça a cláusula passada como argumento (cláusula WHERE)
        E Obter(Expression<Func<E, bool>> where);

        /// Retorna o objeto que satisfaça a cláusula passada como argumento (cláusula WHERE)
        E ObterEntidadeEncerrandoContexto(Expression<Func<E, bool>> where);

        /// Retorna todos os objetos de um tipo
        IQueryable<E> ObterTodos();

        /// Retorna os objetos usando paginação
        IQueryable<E> ObterTodosPaginando(int maximumRows, int startRowIndex);

        /// Retorna todos os objetos que satisfaçam a cláusula passada
        IQueryable<E> ObterSomente(Expression<Func<E, bool>> where);

        /// Retorna todos os objetos que satisfaçam a cláusula passada por predicado
        IQueryable<E> ObterSomenteComPredicado(Expression<Func<E, bool>> where);

        /// Retorna todos os objetos que satisfaçam a cláusula passada encerrando o contexto de dados
        IQueryable<E> ObterSomenteEncerrandoContexto(Expression<Func<E, bool>> where);

        /// Retorna os objetos que satisfaçam a cláusula passada, usando paginação
        IQueryable<E> ObterSomentePaginando(Expression<Func<E, bool>> where, Expression<Func<E, int>> order, int startRowIndex, int maximumRows);

        /// Retorna os objetos que satisfaçam a cláusula passada, usando paginação
        IQueryable<E> ObterSomentePaginando(Expression<Func<E, bool>> where, Expression<Func<E, string>> order, int startRowIndex, int maximumRows);

        /// Retorna os objetos que satisfaçam a cláusula passada, usando paginação
        IQueryable<E> ObterSomentePaginando(Expression<Func<E, bool>> where, Expression<Func<E, decimal>> order, int startRowIndex, int maximumRows);

        /// Retorna todos os objetos que satisfaçam a cláusula passada por um predicado, usando paginação decrescente
        IQueryable<E> ObterUtlilizandoPredicadoPaginandoDecrescente(Expression<Func<E, bool>> where, Expression<Func<E, int>> order, int startRowIndex, int maximumRows);

        /// Retorna todos os objetos que satisfaçam a cláusula passada por um predicado, usando paginação decrescente
        IQueryable<E> ObterUtlilizandoPredicadoPaginandoDecrescente(Expression<Func<E, bool>> where, Expression<Func<E, string>> order, int startRowIndex, int maximumRows);

        /// Retorna todos os objetos que satisfaçam a cláusula passada por um predicado, usando paginação decrescente
        IQueryable<E> ObterUtlilizandoPredicadoPaginandoDecrescente(Expression<Func<E, bool>> where, Expression<Func<E, DateTime>> order, int startRowIndex, int maximumRows);

        /// Retorna todos os objetos que satisfaçam a cláusula passada por um predicado, usando paginação
        IQueryable<E> ObterUtlilizandoPredicadoPaginandoCrescente(Expression<Func<E, bool>> where, Expression<Func<E, int>> order, int startRowIndex, int maximumRows);

        /// Retorna todos os objetos que satisfaçam a cláusula passada por um predicado, usando paginação
        IQueryable<E> ObterUtlilizandoPredicadoPaginandoCrescente(Expression<Func<E, bool>> where, Expression<Func<E, string>> order, int startRowIndex, int maximumRows);

        /// Retorna todos os objetos que satisfaçam a cláusula passada por um predicado, usando paginação
        IQueryable<E> ObterUtlilizandoPredicadoPaginandoCrescente(Expression<Func<E, bool>> where, Expression<Func<E, DateTime>> order, int startRowIndex, int maximumRows);

        /// Retorna a quantidade de objetos persistentes.
        int ContarObjetos();

        /// Retorna a quantidade de objetos persistentes que satisfaçam a cláusula WHERE
        int ContarSomente(Expression<Func<E, bool>> where);

        ///Retorna a quantidade de objetos persistentes que satisfaçam a cláusula WHERE utlilizando predicado
        int ContarUtilizandoPredicado(Expression<Func<E, bool>> where);

        void Desatachar(E entity);

        /// Retorna o objeto que satisfaça a cláusula passada como argumento (cláusula WHERE)
        E ObterUltimo(Expression<Func<E, bool>> where);
    }
}
