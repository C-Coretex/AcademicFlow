using AcademicFlow.Domain.Helpers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AcademicFlow.Domain.Helpers.Base
{
    public class RepositoryBase<TDbContext, TModel>: IRepositoryBase<TModel> where TModel : class, IModel, new()
                                                                                         where TDbContext : DbContextBase<TDbContext>
    {
        protected TDbContext dbContext;
        protected DbSet<TModel> dbSet;

        public RepositoryBase(TDbContext dbContext, DbSet<TModel> dbSet)
        {
            this.dbContext = dbContext;
            this.dbSet = dbSet;
        }

        public virtual IQueryable<TModel> GetAll()
        {
            return dbSet.AsNoTracking();
        }

        public virtual TModel? GetById(int id)
        {
            return dbSet.AsNoTracking().FirstOrDefault(x => x.Id.Equals(id));
        }

        public virtual async Task<TModel?> GetByIdAsync(int id)
        {
            return await dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public virtual TModel Add(TModel model, bool saveChanges = true)
        {
            return dbContext.AddEntity(dbSet, model, saveChanges);
        }
        public virtual Task<TModel> AddAsync(TModel model, bool saveChanges = true)
        {
            return dbContext.AddEntityAsync(dbSet, model, saveChanges);
        }

        public virtual IEnumerable<TModel> AddRange(IEnumerable<TModel> models, bool saveChanges = true)
        {
            return dbContext.AddEntities(dbSet, models, saveChanges);
        }
        public virtual Task<IEnumerable<TModel>> AddRangeAsync(IEnumerable<TModel> models, bool saveChanges = true)
        {
            return dbContext.AddEntitiesAsync(dbSet, models, saveChanges);
        }

        public virtual void Delete(TModel model, bool saveChanges = true)
        {
            dbContext.RemoveEntity(dbSet, model, saveChanges);
        }
        public virtual Task DeleteAsync(TModel model, bool saveChanges = true)
        {
            return dbContext.RemoveEntityAsync(dbSet, model, saveChanges);
        }

        public virtual void DeleteRange(IEnumerable<TModel> models, bool saveChanges = true)
        {
            dbContext.RemoveEntities(dbSet, models, saveChanges);
        }
        public virtual Task DeleteRangeAsync(IEnumerable<TModel> models, bool saveChanges = true)
        {
            return dbContext.RemoveEntitiesAsync(dbSet, models, saveChanges);
        }

        public virtual void Delete(int id, bool saveChanges = true)
        {
            dbContext.RemoveEntity(dbSet, id, saveChanges);
        }
        public virtual Task DeleteAsync(int id, bool saveChanges = true)
        {
            return dbContext.RemoveEntityAsync(dbSet, id, saveChanges);
        }

        public virtual void DeleteRange(IEnumerable<int> ids, bool saveChanges = true)
        {
            dbContext.RemoveEntities(dbSet, ids, saveChanges);
        }
        public virtual Task DeleteRangeAsync(IEnumerable<int> ids, bool saveChanges = true)
        {
            return dbContext.RemoveEntitiesAsync(dbSet, ids, saveChanges);
        }

        public virtual TModel Update(TModel model, bool saveChanges = true)
        {
            return dbContext.UpdateEntity(dbSet, model, saveChanges);
        }
        public virtual Task<TModel> UpdateAsync(TModel model, bool saveChanges = true)
        {
            return dbContext.UpdateEntityAsync(dbSet, model, saveChanges);
        }

        public virtual IEnumerable<TModel> UpdateRange(IEnumerable<TModel> models, bool saveChanges = true)
        {
            return dbContext.UpdateEntities(dbSet, models, saveChanges);
        }
        public virtual Task<IEnumerable<TModel>> UpdateRangeAsync(IEnumerable<TModel> models, bool saveChanges = true)
        {
            return dbContext.UpdateEntitiesAsync(dbSet, models, saveChanges);
        }

        public virtual void SaveChanges()
        {
            dbContext.SaveDBChanges();
        }

        public virtual Task SaveChangesAsync(CancellationToken? cancellationToken = null)
        {
            cancellationToken ??= CancellationToken.None;
            return dbContext.SaveDBChangesAsync(cancellationToken.Value);
        }
    }
}
