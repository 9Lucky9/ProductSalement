using Microsoft.EntityFrameworkCore;
using ProductSalement.Models;
using System;

namespace ProductSalement.Repository
{
    public class SaleRepository : IRepository<Sale>, IDisposable
    {
        private DatabaseContext _context;
        public SaleRepository(DatabaseContext databaseContext)
        {
            _context = databaseContext;
        }
        public void Create(Sale item)
        {
            _context.Sales.Add(item);
            _context.SaveChanges();
        }

        public Sale Get(int id)
        {
            return _context.Sales.Find(id);
        }

        public void Remove(int id)
        {
            _context.Remove(_context.Sales.Find(id));
            _context.SaveChanges();
        }

        public void Update(Sale item)
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
