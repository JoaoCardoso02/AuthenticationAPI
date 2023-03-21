using Microsoft.EntityFrameworkCore;
using AuthenticationAPI.Domain.Entities;
using AuthenticationAPI.Infrastructure.Common.Interfaces;
using Infrastructure.Persistence.Configurations;

namespace AuthenticationAPI.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql("Host=localhost;Database=test;Username=joao;Password=password")
            .UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AccountConfiguration());
    }

    public DbSet<Account> Account { get; set; } 
}
