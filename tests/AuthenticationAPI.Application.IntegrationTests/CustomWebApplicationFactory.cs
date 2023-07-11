using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using AuthenticationAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationAPI.Application.IntegrationTests;

public class FakeApplicationDbContext : ApplicationDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseInMemoryDatabase("in-memory");
    }

}

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {

        base.ConfigureWebHost(builder);

        builder.ConfigureServices((builder, services) =>
        {
            // 	[195]	{AuthenticationAPI.Infrastructure.Common.Interfaces.IApplicationDbContext}	System.RuntimeType
            //var a = services.Select((d) => d.ServiceType);
            //var b = typeof(DbContextOptions<ApplicationDbContext>);
            //var serviceDescriptor = services.SingleOrDefault(d => {
            //    Console.WriteLine(d);
            //    return d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>);
            //});

            //if (serviceDescriptor != null)
            //    services.Remove(serviceDescriptor);
            //ApplicationDbContext fakeApplicationDbContext = new FakeApplicationDbContext();
            //fakeApplicationDbContext.Database.EnsureCreated();
            //services.AddSingleton<ApplicationDbContext>(fakeApplicationDbContext);
        });
    }
}

