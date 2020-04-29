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

using LinqKit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Repository.Architecture
{
    public abstract class RepositorioGenerico<E, C> : IRepositorioGenerico<E>, IDisposable
        where E : class, new()
        where C : DbContext, new()
    {

        protected IDbSet<E> dbSet;
        /// contexto do Entity Framework
        protected C context;

        /// <summary>
        /// Construtor padrão, sem argumentos. Obtém o contexto do 
        /// Entity Framework usando o contextManager.
        /// </summary>
        protected RepositorioGenerico()
        {
            // Obtém o contexto
            context = ContextManager.Getcontext<C>();
            dbSet = context.Set<E>();
            context.Configuration.LazyLoadingEnabled = true;
            context.Configuration.ProxyCreationEnabled = true;
        }

        public E CriarObjeto()
        {
            return dbSet.Create();
        }

        /// <summary>
        /// Insere um novo objeto persistente
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Inserir(E entity)
        {
            dbSet.Add(entity);
            return SalvarAlteracoes();
        }



        /// <summary>
        /// Insere um novo objeto persistente
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool InserirComProxy(E entity)
        {
            var myEntity = CriarObjeto();
            dbSet.Add(myEntity);
            context.Entry(myEntity).CurrentValues.SetValues(entity);
            return SalvarAlteracoes();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool Atualizar(E entity)
        {
            var tracked = context.Set<E>().Find(context.KeyValuesFor(entity));

            if (tracked == null) return false;

            context.Entry(tracked).CurrentValues.SetValues(entity);

            return SalvarAlteracoes();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool AtualizarTodos(List<E> entity)
        {
            return SalvarAlteracoes();
        }

        /// <summary>
        /// Exclui um objeto persistente
        /// </summary>
        public bool Remover(E entity)
        {
            dbSet.Remove(entity);
            return SalvarAlteracoes();
        }

        /// <summary>
        ///  Retorna um objeto que satisfaça a cláusula passada como parâmetro.
        /// </summary>
        public E Obter(Expression<Func<E, bool>> where)
        {
            try
            {
                return dbSet.Where(where).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }


        }

        /// <summary>
        ///  Retorna um objeto que satisfaça a cláusula passada como parâmetro e a retira do contexto.
        /// </summary>
        public E ObterEntidadeEncerrandoContexto(Expression<Func<E, bool>> where)
        {
            try
            {
                return dbSet.Where(where).AsNoTracking().FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Salva as alterações no banco de dados.
        /// </summary>
        protected bool SalvarAlteracoes()
        {
            try
            {
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        ///// <summary>
        ///// Rastreia as alterações nas FK
        ///// </summary>
        //public bool DescobrirAlteracoes()
        //{
        //    try
        //    {
        //        context.ChangeTracker.DetectChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        new NLogLogger().Error(ex);
        //        return false;
        //    }
        //}

        /// <summary>
        ///  Retorna todos os objetos persistentes.
        /// </summary>
        public IQueryable<E> ObterTodos()
        {
            try
            {
                return dbSet;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Retorna todos os objetos usando paginação.
        /// </summary>
        public IQueryable<E> ObterTodosPaginando(int maximumRows, int startRowIndex)
        {
            try
            {
                return dbSet.Skip(startRowIndex).Take(maximumRows);
            }
            catch (Exception)
            {
                return null;
            }
            }

            /// <summary>
            /// Retorna todos os objetos que satisfaçam a cláusula passada
            /// </summary>
            public IQueryable<E> ObterSomente(Expression<Func<E, bool>> where)
            {
                try
                {
                    return dbSet.Where(where);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            /// <summary>
            /// Retorna todos os objetos que satisfaçam a cláusula passada
            /// </summary>
            public IQueryable<E> ObterSomenteComPredicado(Expression<Func<E, bool>> where)
            {
                try
                {
                    return dbSet.AsExpandable().Where(where);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            /// <summary>
            /// Retorna todos os objetos que satisfaçam a cláusula passada
            /// </summary>
            public IQueryable<E> ObterSomenteEncerrandoContexto(Expression<Func<E, bool>> where)
            {
                try
                {
                    return dbSet.Where(where).AsNoTracking();
                }
                catch (Exception)
                {
                    return null;
                }
            }

            /// <summary>
            /// Retorna todos os objetos que satisfaçam a cláusula passada, usando paginação
            /// </summary>
            public IQueryable<E> ObterSomentePaginando(Expression<Func<E, bool>> where, Expression<Func<E, int>> order, int startRowIndex, int maximumRows)
            {
                try
                {
                    return dbSet.Where(where).OrderBy(order).Skip(startRowIndex).Take(maximumRows);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="@where"></param>
            /// <param name="order"></param>
            /// <param name="startRowIndex"></param>
            /// <param name="maximumRows"></param>
            /// <returns></returns>
            public IQueryable<E> ObterSomentePaginando(Expression<Func<E, bool>> @where, Expression<Func<E, string>> order, int startRowIndex, int maximumRows)
            {
                try
                {
                    return dbSet.Where(where).OrderBy(order).Skip(startRowIndex).Take(maximumRows);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            /// <summary>
            /// Retorna todos os objetos que satisfaçam a cláusula passada, usando paginação
            /// </summary>
            public IQueryable<E> ObterSomentePaginando(Expression<Func<E, bool>> where, Expression<Func<E, decimal>> order, int startRowIndex, int maximumRows)
            {
                try
                {
                    return dbSet.Where(where).OrderBy(order).Skip(startRowIndex).Take(maximumRows);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            /// <summary>
            /// Retorna todos os objetos que satisfaçam a cláusula passada por um predicado, usando paginação com ordenação decrescente
            /// </summary>
            IQueryable<E> IRepositorioGenerico<E>.ObterUtlilizandoPredicadoPaginandoDecrescente(Expression<Func<E, bool>> where, Expression<Func<E, int>> order, int startRowIndex, int maximumRows)
            {
                try
                {
                    return dbSet.AsExpandable().Where(where).OrderByDescending(order).Skip(startRowIndex).Take(maximumRows);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            /// <summary>
            /// Retorna todos os objetos que satisfaçam a cláusula passada por um predicado, usando paginação com ordenação decrescente
            /// </summary>
            IQueryable<E> IRepositorioGenerico<E>.ObterUtlilizandoPredicadoPaginandoDecrescente(Expression<Func<E, bool>> where, Expression<Func<E, string>> order, int startRowIndex, int maximumRows)
            {
                try
                {
                    return dbSet.AsExpandable().Where(where).OrderByDescending(order).Skip(startRowIndex).Take(maximumRows);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            /// <summary>
            /// Retorna todos os objetos que satisfaçam a cláusula passada por um predicado, usando paginação com ordenação decrescente
            /// </summary>
            IQueryable<E> IRepositorioGenerico<E>.ObterUtlilizandoPredicadoPaginandoDecrescente(Expression<Func<E, bool>> where, Expression<Func<E, DateTime>> order, int startRowIndex, int maximumRows)
            {
                try
                {
                    return dbSet.AsExpandable().Where(where).OrderByDescending(order).Skip(startRowIndex).Take(maximumRows);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            /// <summary>
            /// Retorna todos os objetos que satisfaçam a cláusula passada por um predicado, usando paginação
            /// </summary>
            public IQueryable<E> ObterUtlilizandoPredicadoPaginandoCrescente(Expression<Func<E, bool>> where, Expression<Func<E, int>> order, int startRowIndex, int maximumRows)
            {
                try
                {
                    return dbSet.AsExpandable().Where(where).OrderBy(order).Skip(startRowIndex).Take(maximumRows);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="where"></param>
            /// <param name="order"></param>
            /// <param name="startRowIndex"></param>
            /// <param name="maximumRows"></param>
            /// <returns></returns>
            public IQueryable<E> ObterUtlilizandoPredicadoPaginandoCrescente(Expression<Func<E, bool>> where, Expression<Func<E, string>> order, int startRowIndex, int maximumRows)
            {
                try
                {
                    return dbSet.AsExpandable().Where(where).OrderBy(order).Skip(startRowIndex).Take(maximumRows);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="where"></param>
            /// <param name="order"></param>
            /// <param name="startRowIndex"></param>
            /// <param name="maximumRows"></param>
            /// <returns></returns>
            public IQueryable<E> ObterUtlilizandoPredicadoPaginandoCrescente(Expression<Func<E, bool>> where, Expression<Func<E, DateTime>> order, int startRowIndex, int maximumRows)
            {
                try
                {
                    return dbSet.AsExpandable().Where(where).OrderBy(order).Skip(startRowIndex).Take(maximumRows);
                }
                catch (Exception)
                {
                    return null;
                }
            }

            /// <summary>
            /// Retorna o número de objetos
            /// </summary>
            /// <returns></returns>
            public int ContarObjetos()
            {
                try
                {
                    return dbSet.Count();
                }
                catch (Exception)
                {
                    return 0;
                }
            }

            /// <summary>
            /// Retorna o número de objetos que satisfaçam a cláusula passada
            /// </summary>
            public int ContarSomente(Expression<Func<E, bool>> where)
            {
                try
                {
                    return dbSet.Where(where).Count();
                }
                catch (Exception)
                {
                    return 0;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="where"></param>
            /// <returns></returns>
            public int ContarUtilizandoPredicado(Expression<Func<E, bool>> where)
            {
                try
                {
                    return dbSet.AsExpandable().Where(where).Count();
                }
                catch (Exception)
                {
                    return 0;
                }
            }

            /// <summary>
            /// Verifica se determinado objeto que satisfaça a condição existe.
            /// </summary>
            public bool Existe(Expression<Func<E, bool>> where)
            {
                return (ContarSomente(@where) != 0);
            }

            /// <summary>
            /// Libera os recursos do Entity Framework.
            /// </summary>
            public void Dispose()
            {
                if (context != null)
                    context.Dispose();
            }

            public void Desatachar(E entity)
            {
                try
                {
                    context.Entry(entity).State = EntityState.Detached;
                }
                catch (Exception)
                {
                }
            }

            public E ObterUltimo(Expression<Func<E, bool>> where)
            {
                try
                {
                    return dbSet.Where(where).AsEnumerable().LastOrDefault();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
