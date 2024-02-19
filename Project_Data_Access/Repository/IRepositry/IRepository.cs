using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Project_Data_Access.Repository
{
    public interface IRepository<T>where T:class
    {
        void Add(T entity); //save
        void Remove(T entity);//Delete
        void Remove(int id);
        void update(T entity);//update
        void RemoveRange(IEnumerable<T> entity);//find

        T get(int Id);
        IEnumerable<T> Getall(
            Expression<Func<T,bool>>filter=null,    
            Func<IQueryable<T>,IOrderedQueryable<T>>orderby=null,
            string includeProperties=null
             );
        T FirstOrDefoult(
              Expression<Func<T, bool>> Filter = null,

              string includeProprties = null);
    }
}
