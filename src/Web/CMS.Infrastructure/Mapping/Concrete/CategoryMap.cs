using CMS.Domain.Entities.Concrete;
using CMS.Infrastructure.Mapping.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Infrastructure.Mapping.Concrete
{
    public class CategoryMap : BaseMap<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired(true);
            builder.Property(x => x.Slug).IsRequired(true);

            builder.HasMany(x => x.Products)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); 


            base.Configure(builder);
        }
    }
}
