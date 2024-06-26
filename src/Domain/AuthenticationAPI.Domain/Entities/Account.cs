using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AuthenticationAPI.Domain.Entities;

public class Account : BaseEntity
{
    public Email Email { get; }

    [JsonIgnore]
    public string Password { get; }

    public Account(Email email, string password)
    {
        Email = email;
        Password = password;
    }

    protected Account() { }
}
