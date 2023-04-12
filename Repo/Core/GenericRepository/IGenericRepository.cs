namespace Repo.Core.GenericRepository
{
    public interface IGenericRepository<T> where T : class,IEntity
    {
       Task <IEnumerable<T>> GetAll(T entity);
        Task<T> GetById(int id);
        Task <T> Add(T entity); 
        Task<T> Update(T entity);
         Task<T> Delete(int id);
    }
}
