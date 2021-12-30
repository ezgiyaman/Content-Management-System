using CMS.Domain.Entities.Interface;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.Repositories.Interface.BaseRepositories
{
    public interface IBaseRepository<T> where T : IBaseEntity
    {
        //Asenkron olması için "Task" 

        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        //GetByDefault & GetByDefaults'ta data çekme işlemleri yapılmaktadır.
        Task<List<T>> GetDefaults(Expression<Func<T, bool>> expression);
        Task<T> GetDefault(Expression<Func<T, bool>> expression);

        //Any :Login işlemler için kullanılan bir methodtur.
        Task<bool> Any(Expression<Func<T, bool>> expression);

        //GetByDefault & GetByDefaults'ta data çekme işlemleri yapılmaktadır.

        //<T,bool> T tipi bool dönüyor şart tutup tutmama gibi.
        //Gelen datayı farklı farklı değerlere atadığım için ve birden fazla tablodan data girdiğim için TResult olarak yazıyorum.(Category,product gibi).

        Task<TResult> GetFilteredFirstOrDefault<TResult>(Expression<Func<T, TResult>> selector,
                                                          Expression<Func<T, bool>> expression,
                                                          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                          Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);


        Task<List<TResult>> GetFilteredList<TResult>(Expression<Func<T, TResult>> selector,
                                                         Expression<Func<T, bool>> expression,
                                                         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                         Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        
    }
}
