using System.ComponentModel.DataAnnotations;
using BookPublisher.Domain.DomainValidation;

namespace BookPublisher.Domain.Entities;

public sealed class Book : BaseEntity
{
    public string Title { get;  private set; }
    public string SubTitle { get;  private set; }
    public int Edition { get;  private set; }
    public DateOnly LaunchDate { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set ; }

    public List<AuthorPerBook> AuthorPerBooks { get; set; }

    public Book(long id, string title, string subTitle, int edition, DateOnly lachDate, 
        decimal price, int quantity)
    {
        DomainExceptionValidation.When(id < 1, "Invalid id value"); 
        Id = id; 
        ValidationDomain(title, edition, lachDate, price, quantity, subTitle);    
    }

    public Book(string title, string subTitle ,int edition, DateOnly lachDate, 
        decimal price, int quantity)
    {
        ValidationDomain(title, edition, lachDate, price, quantity, subTitle);   
    }

    public void Update(string title, int edition, DateOnly launchDate, decimal price,
        int quantity, string subTitle)
    {
        ValidationDomain(title, edition, launchDate, price, quantity, subTitle);
    }

    public Book(string title, int edition, DateOnly launchDate, decimal price,
        int quantity)
    {
        ValidationDomain(title, edition, launchDate, price, quantity); 
    }
    private void ValidationDomain(string title ,int edition, DateOnly launchDate, 
        decimal price, int quantity, string subTitle = null)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(title), 
            "Invalid title. Title is required."); 
        
        DomainExceptionValidation.When(title.Length < 5, 
            "Title must have at least 5 characters");

        DomainExceptionValidation.When(title.Length > 50, 
            "Title must have a maximum of 50 characters");  
        
        DomainExceptionValidation.When(subTitle?.Length < 5, 
            "Subtitle must have at least 5 characters");

        DomainExceptionValidation.When(subTitle?.Length > 50, 
            "Subtitle must have a maximum of 50 characters");

        DomainExceptionValidation.When(edition < 1, "invalid edition value"); 

        DomainExceptionValidation.When(launchDate > DateOnly.FromDateTime(DateTime.Now), 
            "Lauch date cannot be later than the current date");

        DomainExceptionValidation.When(price <= 0, 
            "Price must be greater than zero");

        DomainExceptionValidation.When(quantity < 0, 
            "Quatity must be greater than zero");

        Title = title; 
        SubTitle = subTitle; 
        Edition = edition; 
        LaunchDate = launchDate; 
        Price = price; 
        Quantity = quantity; 

    }
}
