using System.Text.RegularExpressions;

namespace AuthenticationAPI.Domain.ValueObjects;

public class Email : ValueObject
{
    public string Value { get; }

    protected Email() {}

    private Email(string value) { Value = value; }

    public static Email Create(string value) {
        Regex EmailValidation = new("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");

        if (!EmailValidation.IsMatch(value))
            throw new Exception("Email is not valid.");

        return new Email(value);
    }

    public static implicit operator string(Email email) => email.Value;

    public static implicit operator Email(string value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
