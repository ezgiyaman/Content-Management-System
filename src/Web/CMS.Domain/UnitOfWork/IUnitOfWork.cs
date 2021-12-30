using CMS.Domain.Repositories.Interface.EntityTypeRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable //Business'larıma birden fazla repository devreye girecektir,iş bittiğinde bu business içerisine giren repository'lerin memory managment yapmak için IAsyncDisposable sınıfının yeteneklerinden faydalanmak için burada IAsyncDisposable  kalıtım aldım.
    {

        //Bu kısımda işlemleri yapmayı zorunlu tutup,Infrastructure katmanında UnitOfWork'te bu sınıftaki özelliklere gövde kazandırdım.

        //Repositoryleri singletonla ürettim sadece get olarak belirttim.(Nesnelerini çıkarmak istemediğimiz için unitofwork harici bi yerde newlememek için singleton ile ürettim ve memory managment için)

        IAppUserRepository AppUserRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        IPageRepository PageRepository { get; }

        Task Commit(); //birden fazla repository'den gelen bilgileri tek bir seferde db'ye göndermek için kullanacağım.

        Task ExecuteSqlRaw(string sql, params object[] parameters);
    }
}
