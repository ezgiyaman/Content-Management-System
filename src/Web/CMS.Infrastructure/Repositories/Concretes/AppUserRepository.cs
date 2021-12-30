using CMS.Domain.Entities.Concrete;
using CMS.Domain.Repositories.Interface.EntityTypeRepositories;
using CMS.Infrastructure.Context;
using CMS.Infrastructure.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Infrastructure.Repositories.Concretes
{
    public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
    {
        //constructor
        public AppUserRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}
