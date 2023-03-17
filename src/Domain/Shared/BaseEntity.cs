namespace AuthenticationAPI.Domain.Shared;

public class BaseEntity
{
    public int? Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
