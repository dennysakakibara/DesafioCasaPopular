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

using System.Data.Entity;
  
namespace Repository.Architecture
{
    public static class ContextManager
    {
        /// <summary>
        /// Obtém o contexto do Entity Framework usando generics.
        /// </summary>
        public static T Getcontext<T>()
            where T : DbContext
        {
            //string ocKey = "ocm_" + typeof(T).Name;

            //if (HttpContext.Current != null)
            //{
            //    if (HttpContext.Current.Items[ocKey] == null)
            //    {
            //        T ctx = typeof(T).GetConstructor(System.Type.EmptyTypes)
            //                         .Invoke(System.Type.EmptyTypes) as T;

            //        HttpContext.Current.Items.Add(ocKey, ctx);
            //    }

            //    return HttpContext.Current.Items[ocKey] as T;
            //}
            // Caso a aplicação não seja web, instancia e retorna o contexto.
            return typeof(T).GetConstructor(System.Type.EmptyTypes)
                            .Invoke(System.Type.EmptyTypes) as T;
        }
    }
}
