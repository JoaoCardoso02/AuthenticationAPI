namespace AuthenticationAPI.Domain.ValueObjects;

public class Email : ValueObject
{
    public string Value { get; }

    private Email(string value) { Value = value; }

    public static Email Create(string value) {
        return new Email(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
