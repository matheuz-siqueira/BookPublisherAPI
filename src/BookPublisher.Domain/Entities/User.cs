using BookPublisher.Domain.DomainValidation;

namespace BookPublisher.Domain.Entities;

public class User : BaseEntity
{
    public string UserName { get; private set; }
    public string Password { get; private set; }
    public string FullName { get; private set; } 
    public string Email { get; private set; }

    public User(long id, string userName, string password, string fullName)
    {  
        DomainExceptionValidation.When(id < 0, "Invalid id value"); 
        Id = id; 
        ValidationDomain(userName, password, fullName);
    }

    public User(string userName, string password, string fullName)
    {
        ValidationDomain(userName, password, fullName); 
        
    }

    public void UpdatePassword(string password)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(password), "password is required");
        DomainExceptionValidation.When(password.Length < 8, "username must have at least 8 characters");
        Password = password;
    }

    private void ValidationDomain(string userName, string password, string fullName)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(userName), "username is required");
        DomainExceptionValidation.When(userName.Length < 6, "username must have at least 6 characters");
        DomainExceptionValidation.When(userName.Length > 15, "username must have a maximum of 6 characters");

        DomainExceptionValidation.When(string.IsNullOrEmpty(fullName), "fullname is required");
        DomainExceptionValidation.When(userName.Length < 3, "fullname must have at least 3 characters");
        DomainExceptionValidation.When(userName.Length > 40, "fullname must have a maximum of 40 characters");

        DomainExceptionValidation.When(string.IsNullOrEmpty(password), "password is required");
        DomainExceptionValidation.When(password.Length < 8, "username must have at least 8 characters");
    
        UserName = userName; 
        Password = password; 
        FullName = fullName; 
    }
}
