namespace AuthenticationAPI.Domain.Entities;

public class AuthPayload
{
	public int Id { get; set; }
	public Email Email { get; set; }

	public AuthPayload(int id, Email email)
	{
        Id = id;
		Email = email;
	}
}
