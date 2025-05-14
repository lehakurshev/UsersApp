using System.Text.RegularExpressions;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.TypeConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Guid);
        builder.HasIndex(user => user.Guid).IsUnique();
        builder.HasIndex(user => user.Login).IsUnique();
        
        builder.Property(u => u.Login)
            .IsRequired()
            .HasConversion(v => v, v => Regex.Replace(v, "[^a-zA-Z0-9]", ""));


        builder.Property(u => u.Password)
            .IsRequired()
            .HasConversion(v => v, v => Regex.Replace(v, "[^a-zA-Z0-9]", ""));

        builder.Property(u => u.Name)
            .IsRequired()
            .HasConversion(v => v, v => Regex.Replace(v, "[^a-zA-Zа-яА-Я]", ""));
    }
}