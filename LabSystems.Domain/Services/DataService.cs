using System.Collections.Generic;
using System.Threading.Tasks;
using LabSystems.Domain.Models;
using LabSystems.Domain.Context;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using System;

namespace LabSystems.Domain.Services
{
    public interface IDataService<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(Guid id);

        Task<T> Create(T entity);

        Task<T> Update(Guid id, T entity);

        Task<bool> Delete(Guid id);

        Task<T> CreateOrUpdate(Guid id, T entity);
    }

    public static class DbSetExtensions
    {
        public static void CreateOrUpdateCollection<T>(ICollection<T> existing, ICollection<T> entity, DbContext context) where T : DomainObject
        {
            // Remove any previously encountered entries that are not longer present (pulled from the system?)
            foreach (T e in existing)
            {
                if (!entity.Any(d => d.Id == e.Id))
                {
                    existing.Remove(e);
                }
            }

            foreach (T e in entity)
            {
                T found = existing.SingleOrDefault(d => d.Id == e.Id);
                if(found == null)
                {
                    existing.Add(e);
                }
                else
                {
                    e.Id = found.Id;
                    context.Entry(found).CurrentValues.SetValues(e);
                }
            }
        }

        public static EntityEntry<T> CreateOrUpdate<T>(this DbSet<T> dbSet, T entity, DbContext context, Expression<Func<T, bool>> predicate = null) where T : DomainObject
        {
            bool exists = predicate != null ? dbSet.Any(predicate) : dbSet.Any();

            if (!exists)
            {
                return dbSet.Add(entity);
            }

            // Get a little weird...
            T existing = dbSet.First<T>(predicate);
            entity.Id = existing.Id;

            context.Entry(existing).CurrentValues.SetValues(entity);
            return dbSet.Update(existing);
        }
    }

    public class GenericDataService<T> : IDataService<T> where T : DomainObject
    {
        protected readonly LabSystemsContextFactory _contextFactory;

        public GenericDataService(LabSystemsContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<T> Create(T entity)
        {
            using (LabSystemsContext context = _contextFactory.CreateDbContext())
            {
                EntityEntry<T> createdResult = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();

                return createdResult.Entity;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            using (LabSystemsContext context = _contextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<T> Get(Guid id)
        {
            using (LabSystemsContext context = _contextFactory.CreateDbContext())
            {
                context.DiskDrives.Load();

                T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using (LabSystemsContext context = _contextFactory.CreateDbContext())
            {
                context.DiskDrives.Load();

                IEnumerable<T> entities = await context.Set<T>().ToListAsync();
                return entities;
            }
        }

        public async Task<T> Update(Guid id, T entity)
        {
            using (LabSystemsContext context = _contextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
        }

        public async Task<T> CreateOrUpdate(Guid id, T entity)
        {
            using (LabSystemsContext context = _contextFactory.CreateDbContext())
            {
                context.DiskDrives.Load();

                entity.Id = id;
                EntityEntry<T> result = context.Set<T>().CreateOrUpdate<T>(entity, context, e => e.Id == entity.Id);
                await context.SaveChangesAsync();

                return result.Entity;
            }
        }
    }
}
