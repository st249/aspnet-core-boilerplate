using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.Infrastructure.Extentions;

internal static class EnumerationConfiguration
{
    public static void OwnEnumeration<TEntity, TEnum>(this EntityTypeBuilder<TEntity> builder,
        Expression<Func<TEntity, TEnum>> property)
        where TEntity : class
        where TEnum : Enumeration
    {
        builder
            .Property(property)
            .HasConversion(x => x.Id, x => Enumeration.FromValue<TEnum>(x));
    }
}



