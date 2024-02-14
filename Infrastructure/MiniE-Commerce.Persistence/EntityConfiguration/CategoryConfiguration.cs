using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniE_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commerce.Persistence.EntityConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {

            builder.HasData(
    new
    {
        Id = 1,
        Name = "BaseCategory",
        ParentCategoryID = (int?)null,
        IsDeleted = false,
        CreatedDate = DateTime.Now

    }) ;


            builder.Property(x => x.Name).HasMaxLength(256);
            builder
.HasMany(c => c.Subcategories)
.WithOne(c => c.ParentCategory)
.HasForeignKey(c => c.ParrentCategoryId);
        }
    }
}
