using BookPublisher.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookPublisher.Infra.Data.EntitiesConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(t => t.Id); 
        builder.Property(t => t.UserName).HasMaxLength(15).IsRequired();
        builder.Property(t => t.FullName).HasMaxLength(40).IsRequired();
        builder.Property(t => t.Password).IsRequired();
        builder.Property(t => t.Email).IsRequired(); 
    }
}
