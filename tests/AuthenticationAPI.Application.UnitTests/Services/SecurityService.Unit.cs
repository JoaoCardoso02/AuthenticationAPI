using AuthenticationAPI.Application.Services;
using AuthenticationAPI.Domain.Entities;
using AuthenticationAPI.Domain.ValueObjects;
using Microsoft.Extensions.Configuration;

namespace AuthenticationAPI.Application.UnitTests;

public class SecurityServiceUnit
{
    private readonly IConfiguration _configuration = new ConfigurationBuilder()
        .AddInMemoryCollection(new Dictionary<string, string> {
                { "AccessTokenSecurityKey", "MyUltraMegaBlasterAccessTokenSecurityKey" },
        }).Build();

    [Test]
    public void Should_ReturnAValidAccessToken_When_AccountIsValid()
    {
        SecurityService securityService = new SecurityService(_configuration);

        Email email = Email.Create("fake@email.com");
        Account account = new(email, "some-password")
        {
            Id = 1
        };

        string accessToken = securityService.GenerateAccessToken(account);
        Assert.That(accessToken, Is.Not.Empty);
    }

    [Test]
    public void Should_ReturnAValidPayload_When_AccessTokenIsValid()
    {
        SecurityService securityService = new SecurityService(_configuration);

        Email email = Email.Create("fake@email.com");
        int id = 1;
        Account account = new(email, "some-password")
        {
            Id = id
        };

        string accessToken = securityService.GenerateAccessToken(account);

        AuthPayload authPayload = securityService.GetPayload(accessToken);

        Assert.That(authPayload, Is.TypeOf<AuthPayload>());
        Assert.Multiple(() =>
        {
            Assert.That(authPayload.Email.Value, Is.EqualTo(email.Value));
            Assert.That(authPayload.Id, Is.EqualTo(id));
        });
    }
}