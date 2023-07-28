using AuthenticationAPI.Domain.Entities;
using AuthenticationAPI.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.Domain.UnitTests.Entities;

internal class AuthPayloadUnit
{
    [Test]
    public void ShouldInstanceAuthPayloadSuccessfully()
    {
        int id = 1;
        Email email = Email.Create("valid@email.com");
        AuthPayload payload = new AuthPayload(id, email);

        Assert.IsInstanceOf<AuthPayload>(payload);
    }

    [Test]
    public void ShouldReturnIdSuccessfully()
    {
        int id = 1;
        Email email = Email.Create("valid@email.com");
        AuthPayload payload = new AuthPayload(id, email);

        Assert.Multiple(() => {
            Assert.IsNotNull(payload.Id);
            Assert.That(id, Is.EqualTo(payload.Id));
        });
    }

    [Test]
    public void ShouldReturnEmailSuccessfully()
    {
        int id = 1;
        Email email = Email.Create("valid@email.com");
        AuthPayload payload = new AuthPayload(id, email);

        Assert.Multiple(() => {
            Assert.IsNotEmpty(payload.Email);
            Assert.That(email, Is.EqualTo(payload.Email));
        });
    }
}
