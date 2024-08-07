﻿using AuthenticationAPI.Domain.Entities;
using AuthenticationAPI.Domain.ValueObjects;

namespace AuthenticationAPI.Domain.UnitTests.Entities;

public class AccountUnit
{
    [Test]
    public void Should_InstanceAccount_Successfully() {
        Email email = Email.Create("valid@email.com");
        Account account = new Account(email, "strong-password");

        Assert.IsInstanceOf<Account>(account);
    }

    [Test]
    public void Should_ReturnEmail_Successfully() {
        Email email = Email.Create("valid@email.com");
        Account account = new Account(email, "strong-password");

        Assert.Multiple((TestDelegate)(() => {
            Assert.IsNotEmpty(account.Email);
            Assert.That(email, Is.EqualTo((object)account.Email));
        }));
    }

    [Test]
    public void Should_ReturnPassword_Successfully() {
        Email email = Email.Create("valid@email.com");
        string password = "strong-password";
        Account account = new Account(email, password);

        Assert.Multiple(() => {
            Assert.IsNotEmpty(account.Password);
            Assert.That(password, Is.EqualTo(account.Password));
        });
    }

    [Test]
    public void Should_InitCreatedAtField_Successfully() {
        Email email = Email.Create("valid@email.com");
        Account account = new Account(email, "strong-password");

        Assert.Multiple(() => {
            Assert.NotNull(account.CreatedAt);
            Assert.IsInstanceOf<DateTime>(account.CreatedAt);
        });
    }

    [Test]
    public void Should_ChangeOnlyWritableFields_Successfully() {
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

