using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using Repo.Core.Interface;

namespace Repo.Core.GenericRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAll()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products.FindAsync(id);
        }
        public async Task Add(Product model)
        {
            await _context.Products.AddAsync(model);
            await save();
        }



        public async Task Update(Product model)
        {
            var product = await _context.Products.FindAsync(model.Id);
            if (product != null)
            {
                product.BookName = model.BookName;
                product.Price = model.Price;
                product.Qty = model.Qty;
                _context.Update(product);
                await save();
            }
        }
        public async Task Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await save();
            }
        }
        private async Task save()
        {
            await _context.SaveChangesAsync();
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
        }
    }
}
