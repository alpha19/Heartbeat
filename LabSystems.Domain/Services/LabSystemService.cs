using LabSystems.Domain.Context;
using LabSystems.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LabSystems.Domain.Services
{
    public interface ILabSystemService : IDataService<LabSystem>
    {
        Task<LabSystem> CreateOrUpdate(string hostName, LabSystem item);
    }

    public static class DbSetExtensionsLabSystem
    {
        // This is pretty gross and didn't work out how I hoped... Refactor one day but sick of it for now.
        public static void CreateOrUpdateCollection(ICollection<DiskDrive> existing, ICollection<DiskDrive> entity, DbContext context)
        {
            // Remove any previously encountered entries that are not longer present (pulled from the system?)
            // ToList creates a copy and prevents loop corruption..
            foreach (DiskDrive e in existing.ToList())
            {
                if (!entity.Any(d => d.SerialNumber == e.SerialNumber))
                {
                    existing.Remove(e);
                }
            }

            foreach (DiskDrive e in entity)
            {
                DiskDrive found = existing.SingleOrDefault(d => d.SerialNumber == e.SerialNumber);
                if (found == null)
                {
                    existing.Add(e);
                }
                else
                {
                    e.Id = found.Id;
                    e.LabSystemId = found.LabSystemId;

                    context.Entry(found).CurrentValues.SetValues(e);
                }
            }
        }

        public static EntityEntry<LabSystem> CreateOrUpdate(this DbSet<LabSystem> dbSet, LabSystem entity, DbContext context, Expression<Func<LabSystem, bool>> predicate = null)
        {
            bool exists = predicate != null ? dbSet.Any(predicate) : dbSet.Any();

            if (!exists)
            {
                return dbSet.Add(entity);
            }

            // Get a little weird...
            LabSystem existing = dbSet.First<LabSystem>(predicate);
            entity.Id = existing.Id;
            context.Entry(existing).CurrentValues.SetValues(entity);

            CreateOrUpdateCollection(existing.DiskDrives, entity.DiskDrives, context);

            return dbSet.Update(existing);
        }
    }

    public class LabSystemService : GenericDataService<LabSystem>, ILabSystemService
    {
        public LabSystemService(LabSystemsContextFactory contextFactory) : base(contextFactory)
        {
        }

        public async Task<LabSystem> CreateOrUpdate(string hostName, LabSystem entity)
        {
            using (LabSystemsContext context = _contextFactory.CreateDbContext())
            {
                context.DiskDrives.Load();

                entity.HostName = hostName;
                EntityEntry<LabSystem> result = context.Set<LabSystem>().CreateOrUpdate(entity, context, e => e.HostName == entity.HostName);
                await context.SaveChangesAsync();

                return result.Entity;
            }
        }
    }
}
