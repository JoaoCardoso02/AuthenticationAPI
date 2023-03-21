using System;
using AuthenticationAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(ac => ac.Id);
        builder.OwnsOne(ac => ac.Email, sb =>
        {
            sb
                .Property(e => e.Value)
                .HasMaxLength(255)
                .IsRequired(true)
                .HasColumnName("email");
        });
        builder
            .Property(ac => ac.Password)
            .HasMaxLength(255)
            .IsRequired(true);
        builder
            .Property(ac => ac.CreatedAt)
            .HasDefaultValueSql("NOW()");
    }
}

