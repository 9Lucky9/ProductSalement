using Microsoft.EntityFrameworkCore;
using ProductSalement.Models;
using System;

namespace ProductSalement.Repository
{
    public class BuyerRepository : IRepository<Buyer>, IDisposable
    {
        private DatabaseContext _context;
        public BuyerRepository(DatabaseContext databaseContext)
        {
            _context = databaseContext;
        }

        public void Create(Buyer item)
        {
            _context.Buyers.Add(item);
            _context.SaveChanges();
        }

        public Buyer Get(int id)
        {
            return _context.Buyers.Find(id);
        }

        public void Remove(int id)
        {
            _context.Remove(_context.Buyers.Find(id));
            _context.SaveChanges();
        }

        public void Update(Buyer item)
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
