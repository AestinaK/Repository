using Repo.Core.GenericRepository;

namespace Repo.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        public IGenericRepository<Product> ?productRepository;
        public IGenericRepository<Product> ProductRepository
        {
            get
            {
                if (this.productRepository == null)
                {
                    this.productRepository = new GenericRepository<Product>(_context);
                }
                return productRepository;
            }
        }

        
        private bool dispose = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.dispose)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.dispose = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            //throw new NotImplementedException();
        }
        public void save()
        {
            _context.SaveChanges();
           // throw new NotImplementedException();
        }
    }
}
