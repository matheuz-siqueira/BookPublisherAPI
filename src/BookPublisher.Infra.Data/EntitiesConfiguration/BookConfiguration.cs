using BookPublisher.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookPublisher.Infra.Data.EntitiesConfiguration;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(t => t.Id); 
        builder.Property(p => p.Title).HasMaxLength(50).IsRequired();
        builder.Property(p => p.SubTitle).HasMaxLength(100); 
        builder.Property(p => p.Quantity).IsRequired();  
        builder.Property(p => p.Price).HasPrecision(8, 2).IsRequired(); 
        builder.Property(p => p.LaunchDate).IsRequired(); 
        builder.Property(p => p.Edition).IsRequired(); 
    }
}
