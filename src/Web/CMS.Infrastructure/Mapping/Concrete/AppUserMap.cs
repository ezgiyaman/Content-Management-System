using CMS.Domain.Entities.Concrete;
using CMS.Infrastructure.Mapping.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Infrastructure.Mapping.Concrete
{
    public class AppUserMap : BaseMap<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            //Aşağıdaki 3 özellikte Identity'den gelmektedir.Identity'den gelen gömülü özelliklerde mapping edilebilir.
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserName).IsRequired(true);
            builder.Property(x => x.NormalizedUserName).IsRequired(false);


            builder.Property(x => x.FullName).IsRequired(true);
            builder.Property(x => x.Imagepath).IsRequired(false); 

            base.Configure(builder);

        }
    }
}
