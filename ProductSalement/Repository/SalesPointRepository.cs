using Microsoft.EntityFrameworkCore;
using ProductSalement.Models;
using System;
using System.Linq;

namespace ProductSalement.Repository
{
    public class SalesPointRepository : IRepository<SalesPoint>, IDisposable
    {
        private DatabaseContext _context;

        public SalesPointRepository(DatabaseContext databaseContext)
        {
            _context = databaseContext;
        }
        public void Create(SalesPoint item)
        {
            _context.SalesPoints.Add(item);
            _context.SaveChanges();
        }

        public SalesPoint Get(int id)
        {
            return _context.SalesPoints.Include(s => s.ProvidedProducts).ThenInclude(s => s.Product).FirstOrDefault(x => x.Id == id);
        }

        public void Remove(int id)
        {
            _context.Remove(_context.SalesPoints.Find(id));
            _context.SaveChanges();
        }

        public void Update(SalesPoint item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
        public Product FindProduct(int productId)
        {
            return _context.Products.Find(productId);
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
