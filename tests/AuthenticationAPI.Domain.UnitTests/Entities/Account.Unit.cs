using AuthenticationAPI.Domain.Entities;
using AuthenticationAPI.Domain.ValueObjects;

namespace AuthenticationAPI.Domain.UnitTests.Entities;

public class AccountUnit
{
    [Test]
    public void ShouldInstanceAccountSuccessfully() {
        Email email = Email.Create("valid@email.com");
        Account account = new Account(email, "strong-password");

        Assert.IsInstanceOf<Account>(account);
    }

    [Test]
    public void ShouldReturnEmailSuccessfully() {
        Email email = Email.Create("valid@email.com");
        Account account = new Account(email, "strong-password");

        Assert.Multiple(() => {
            Assert.IsNotEmpty(account.Email);
            Assert.That(email, Is.EqualTo(account.Email));
        });
    }

    [Test]
    public void ShouldReturnPasswordSuccessfully() {
        Email email = Email.Create("valid@email.com");
        string password = "strong-password";
        Account account = new Account(email, password);

        Assert.Multiple(() => {
            Assert.IsNotEmpty(account.Password);
            Assert.That(password, Is.EqualTo(account.Password));
        });
    }

    [Test]
    public void ShouldInitCreatedAtFieldAsCurrentDateTime() {
        Email email = Email.Create("valid@email.com");
        Account account = new Account(email, "strong-password");

        Assert.Multiple(() => {
            Assert.NotNull(account.CreatedAt);
            Assert.IsInstanceOf<DateTime>(account.CreatedAt);
        });
    }

    [Test]
    public void ShouldChangeOnlyWritableFieldsSucessfully() {
        Email email = Email.Create("valid@email.com");
        Account account = new Account(email, "strong-password");

        int? OldId = account.Id;
        DateTime OldCreatedAt = account.CreatedAt;
        DateTime? OldDeletedAt = account.DeletedAt;

        account.Id = 2;
        account.CreatedAt = DateTime.Now;
        account.DeletedAt = DateTime.Now;

        Assert.Multiple(() => {
            Assert.That(OldId, Is.Not.EqualTo(account.Id));
            Assert.That(OldCreatedAt, Is.Not.EqualTo(account.CreatedAt));
            Assert.That(OldDeletedAt, Is.Not.EqualTo(account.DeletedAt));

            Assert.That(account.Id, Is.EqualTo(2));
            Assert.NotNull(account.CreatedAt);
            Assert.NotNull(account.DeletedAt);
        });
    }
}

