using Microsoft.EntityFrameworkCore;
using AuthenticationAPI.Domain.Entities;
using Infrastructure.Persistence.Configurations;

namespace AuthenticationAPI.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly Action<ApplicationDbContext, ModelBuilder>? _modelCustomizer;

    #region Constructors
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, Action<ApplicationDbContext, ModelBuilder>? modelCustomizer = null)
        : base(options)
    {
        _modelCustomizer = modelCustomizer;
    }

    #endregion

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder
                .UseNpgsql("Host=localhost;Database=test;Username=joao;Password=password")
                .UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AccountConfiguration());

        if (_modelCustomizer is not null)
        {
            _modelCustomizer(this, modelBuilder);
        }
    }

    public DbSet<Account> Account { get; set; }
}
