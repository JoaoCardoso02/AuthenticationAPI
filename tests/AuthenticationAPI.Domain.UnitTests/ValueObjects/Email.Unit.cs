using AuthenticationAPI.Domain.ValueObjects;

namespace AuthenticationAPI.Domain.UnitTests.ValueObjects;

public class EmailUnit
{
    [Test]
    public void Should_ReturnAValidEmailValue_When_EmailIsValid()
    {
        Email email = Email.Create("itsasimpleemail@email.com");

        Assert.IsNotNull(email.Value);
    }

    [Test]
    public void Should_BeTheSameEmail_When_EmailsAreEquals()
    {
        Email email1 = Email.Create("itsasimpleemail@email.com");
        Email email2 = Email.Create("itsasimpleemail@email.com");

        Assert.IsTrue(email1.Equals(email2));
    }

    [Test]
    public void Should_BeTheSameEmail_When_EmailsAreNotEquals()
    {
        Email email1 = Email.Create("itsasimpleemail@email.com");
        Email email2 = Email.Create("itsadifferentemail@email.com");

        Assert.IsFalse(email1.Equals(email2));
    }

    [Test]
    public void Should_Throw_When_EmailIsInvalid()
    {
        Assert.Throws<Exception>(() => Email.Create("invalid-email..."));
    }
}