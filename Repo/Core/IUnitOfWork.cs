using Microsoft.EntityFrameworkCore.Diagnostics;
using Repo.Core.GenericRepository;

namespace Repo.Core
{
    public interface IUnitOfWork:IDisposable
    {
        //register your repository here
        IGenericRepository<Product> ProductRepository { get; }

        void save();
    }
}
