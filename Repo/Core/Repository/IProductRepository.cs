namespace Repo.Core.Interface
{
    public interface IProductRepository:IDisposable
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(int id);
        Task Add(Product model);
        
        Task Update(Product model);
        Task Delete(int id);

    }
}
