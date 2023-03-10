namespace AuthenticationAPI.Domain.Entities;

public class Account : BaseEntity
{
    public Email Email { get; set; }

    public string Password { get; set; }
}
