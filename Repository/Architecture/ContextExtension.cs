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
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Repository.Architecture
{
    public static class ContextExtension
    {
        public static object[] KeyValuesFor(this DbContext context, object entity)
        {
            Contract.Requires(context != null);
            Contract.Requires(entity != null);

            var entry = context.Entry(entity);
            return context.KeysFor(entity.GetType())
                .Select(k => entry.Property(k).CurrentValue)
                .ToArray();
        }

        public static IEnumerable<string> KeysFor(this DbContext context, Type entityType)
        {
            Contract.Requires(context != null);
            Contract.Requires(entityType != null);

            entityType = ObjectContext.GetObjectType(entityType);

            var metadataWorkspace = ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;
            var objectItemCollection = (ObjectItemCollection)metadataWorkspace.GetItemCollection(DataSpace.OSpace);

            var ospaceType = metadataWorkspace.GetItems<EntityType>(DataSpace.OSpace).SingleOrDefault(t => objectItemCollection.GetClrType(t) == entityType);

            if (ospaceType == null)
            {
                throw new ArgumentException(string.Format("The type '{0}' is not mapped as an entity type.", entityType.Name), "entityType");
            }

            return ospaceType.KeyMembers.Select(k => k.Name);
        }
    }
}
