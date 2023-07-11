namespace AuthenticationAPI.Domain.Shared;

public class BaseEntity
{
    public int? Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? DeletedAt { get; set; }
}
