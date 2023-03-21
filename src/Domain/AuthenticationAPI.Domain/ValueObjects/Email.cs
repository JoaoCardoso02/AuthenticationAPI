namespace AuthenticationAPI.Domain.ValueObjects;

public class Email : ValueObject
{
    public string Value { get; }

    protected Email() {}

    private Email(string value) { Value = value; }

    public static Email Create(string value) {
        return new Email(value);
    }

    public static implicit operator string(Email email) => email.Value;

    public static implicit operator Email(string value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
