using AuthenticationAPI.Domain.ValueObjects;

namespace Domain.UnitTests.ValueObjects;

public class EmailUnit
{
    [Test]
    public void ShouldReturnAValidEmailValue()
    {
        Email email = Email.Create("itsasimpleemail@email.com");

        Assert.IsNotNull(email.Value);
    }

    [Test]
    public void ShouldBeEqualValues()
    {
        Email email1 = Email.Create("itsasimpleemail@email.com");
        Email email2 = Email.Create("itsasimpleemail@email.com");

        Assert.IsTrue(email1.Equals(email2));
    }

    [Test]
    public void ShouldNotBeEqualValues()
    {
        Email email1 = Email.Create("itsasimpleemail@email.com");
        Email email2 = Email.Create("itsadifferentemail@email.com");

        Assert.IsFalse(email1.Equals(email2));
    }
}