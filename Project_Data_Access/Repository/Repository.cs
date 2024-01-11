using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Project_Data_Access.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project_Data_Access.Repository
{
    public class Repository<T> : IRepository<T>where T:class

    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> DbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            DbSet= _context.Set<T>();  
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public T FirstOrDefoult(Expression<Func<T, bool>> Filter = null, string includeProprties = null)
        {
            IQueryable<T> query = DbSet;
           if(Filter!=null)
                query = query.Where(Filter);
           if(includeProprties!=null)
            {
                foreach(var includePropertie in includeProprties.Split(new[] { ',' },StringSplitOptions.RemoveEmptyEntries))
                {
                    query=query.Include(includePropertie);
                }

            }
            return query.FirstOrDefault();
            


        }


        public T get(int Id)
        {
            return DbSet.Find(Id);
        }

        public IEnumerable<T> Getall(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, string includeProperties = null)
        {
            IQueryable<T> query = DbSet;
            if(filter!=null)
                query = query.Where(filter);
            if(includeProperties!=null)
            {
                foreach(var includePropertie in includeProperties.Split(new[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query=query.Include(includePropertie);
                }
            }
            if(orderby!=null)
            
                return orderby(query).ToList();
            return query.ToList();
                
            
           
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }

        public void Remove(int id)
        {
            var entity = DbSet.Find(id);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            DbSet.RemoveRange(entity);
        }

        public void update(T entity)
        {
            _context.ChangeTracker.Clear();
            DbSet.Update(entity);
        }
    }
}
