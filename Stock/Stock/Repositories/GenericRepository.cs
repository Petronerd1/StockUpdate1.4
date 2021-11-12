using Data.DataDb;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class GenericRepository<T> where T : class, new()
    {
        StockTrackingDbContext c = new StockTrackingDbContext();
        public List<T> TList()
        {
            return c.Set<T>().ToList();
        }
        public void TAdd(T g)
        {
            c.Set<T>().Add(g);
            c.SaveChanges();
        }
        public void TUpdate(T g)
        {
            c.Set<T>().Update(g);
            c.SaveChanges();
        }
        public void TDelete(T g)
        {
            c.Set<T>().Remove(g);
            c.SaveChanges();
        }
        public T TGet(int id)
        {
            return c.Set<T>().Find(id);
        }
        public List<T> TList(string l)
        {
            return c.Set<T>().Include(l).ToList();
        }
        public List<T> List(Expression<Func<T,bool>> filter)
        {
            return c.Set<T>().Where(filter).ToList();
        }
       
    }
}
