using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationAPI.Domain.Entities;

public class Account : BaseEntity
{
    public Email Email { get; }

    public string Password { get; }

    public Account(Email email, string password)
    {
        Email = email;
        Password = password;
    }

    protected Account() { }
}
