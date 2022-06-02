using Microsoft.EntityFrameworkCore;
using ProductSalement.Models;
using System;

namespace ProductSalement.Repository
{
    public class ProductRepository : IRepository<Product>, IDisposable
    {
        private DatabaseContext _context;

        public ProductRepository(DatabaseContext context)
        {
            _context = context;

        }
        public void Create(Product item)
        {
            _context.Products.Add(item);
            _context.SaveChanges();
        }

        public Product Get(int id)
        {
            return _context.Products.Find(id);
        }

        public void Remove(int id)
        {
            _context.Remove(_context.Products.Find(id));
            _context.SaveChanges();
        }

        public void Update(Product item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
