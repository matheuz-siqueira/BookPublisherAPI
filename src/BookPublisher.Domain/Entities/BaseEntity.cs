using System.ComponentModel.DataAnnotations;

namespace BookPublisher.Domain.Entities;

public abstract class BaseEntity
{
    [Key]
    public long Id { get; set; }
}
