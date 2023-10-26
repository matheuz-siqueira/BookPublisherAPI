
using BookPublisher.Domain.DomainValidation;

namespace BookPublisher.Domain.Entities;

public class Author : BaseEntity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Gender { get; private set; }

    public Author(string firstName, string lastName, string gender)
    {
        ValidationDomain(firstName, lastName, gender);
    }

    public Author(int id, string firstName, string lastName, string gender)
    {
        DomainExceptionValidation.When(id < 1, "Invalid id value");
        Id = id;
        ValidationDomain(firstName, lastName, gender);
    }

    public void Update(string firstName, string lastName, string gender)
    {
        ValidationDomain(firstName, lastName, gender);
    }

    private void ValidationDomain(string firstName, string lastName, string gender)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(firstName), 
            "Invalid first name. First name is required");

        DomainExceptionValidation.When(firstName.Length > 25, 
            "Invalid first name. Maximum 25 characters");

        DomainExceptionValidation.When(firstName.Length < 3, 
            "Invalid first name. Minimum 3 characters");


        DomainExceptionValidation.When(string.IsNullOrEmpty(lastName), 
            "Invalid last name. Last name is required");

        DomainExceptionValidation.When(firstName.Length > 25, 
            "Invalid last name. Maximum 25 characters");

        DomainExceptionValidation.When(firstName.Length < 3, 
            "Invalid last name. Minimum 3 characters");

        DomainExceptionValidation.When(gender != "Male" && gender != "Female", 
            "gender must be male or female");

        FirstName = firstName; 
        LastName = lastName; 
        Gender = gender; 
    }
}   
