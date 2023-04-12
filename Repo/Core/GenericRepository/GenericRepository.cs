using Microsoft.EntityFrameworkCore;

namespace Repo.Core.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
        
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<T> dbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.dbSet= context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAll(T entity)
        {
            return await context.Set<T>().ToListAsync();
        }

        public async  Task<T> GetById(int id)
        {
            return await context.Set<T>().FindAsync(id); ;
        }
        public async Task<T> Add(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await save();
            return entity;
        }

        public async Task<T> Delete(int id)
        {
            var entity = await context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                context.Set<T>().Remove(entity);
                await save();
                
            }
            return entity;
        }


        public async Task<T> Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await save();
            return entity;
        }

        private async Task save()
        {
            await context.SaveChangesAsync();
        }
    }
}
