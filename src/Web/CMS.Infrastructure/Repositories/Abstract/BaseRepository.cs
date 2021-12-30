using CMS.Domain.Entities.Interface;
using CMS.Domain.Repositories.Interface.BaseRepositories;
using CMS.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Repositories.Abstract
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {
        private readonly AppDbContext _context;
        protected DbSet<T> table;

        public BaseRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
            table = _context.Set<T>();
        }

        public async Task Add(T entity)
        {
            await table.AddAsync(entity);
        }

        //Update ve Delete asenkron olarak çalışmamaktadır o yüzden task olarak değil void olarak işaretledim.
        public void Delete(T entity)
        {
            // table.Remove(entity); => bunu kaldırdım çünkü remove yapmak değil passive yapmak istiyoruz.
        }

        
        public void Update(T entity) => _context.Entry<T>(entity).State = EntityState.Modified;

      
        public async Task<bool> Any(Expression<Func<T, bool>> expression) => await table.AnyAsync(expression);

        public async Task<T> GetDefault(Expression<Func<T, bool>> expression) => await table.FirstOrDefaultAsync(expression);

        public async Task<List<T>> GetDefaults(Expression<Func<T, bool>> expression) => await table.Where(expression).ToListAsync();

    

        public async Task<TResult> GetFilteredFirstOrDefault<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> expression, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = table;
            if (include != null)
            {
                query = include(query);
            }
            if (expression != null) //query büyüte büyüte gidiliyor await yok.
            {
                query = query.Where(expression);
            }
            if (orderBy != null) //siparişler için
            {
                return await orderBy(query).Select(selector).FirstOrDefaultAsync();
            }
            else
            {
                return await query.Select(selector).FirstOrDefaultAsync();
            }
        }

        public async Task<List<TResult>> GetFilteredList<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> expression, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = table;
            if (include != null)
            {
                query = include(query);
            }
            if (expression != null)
            {
                query = query.Where(expression);
            }
            if (orderBy != null)
            {
                return await orderBy(query).Select(selector).ToListAsync();
            }
            else //nullsa query dön
            {
                return await query.Select(selector).ToListAsync();
            }
        }
    }
       
}
