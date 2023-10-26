using BookPublisher.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookPublisher.Infra.Data.EntitiesConfiguration;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(p => p.FirstName).HasMaxLength(25).IsRequired();
        builder.Property(p => p.LastName).HasMaxLength(25).IsRequired();

        builder.HasData(
            new Author(1, "Marcos", "Da Silva", "Male"),
            new Author(2, "Laurene", "Nobre", "Female") 
        );
    }
}
