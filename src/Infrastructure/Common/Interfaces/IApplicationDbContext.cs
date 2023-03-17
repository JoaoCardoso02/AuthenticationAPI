using AuthenticationAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAPI.Infrastructure.Common.Interfaces;

public interface IApplicationDbContext
{
	DbSet<Account> Account { get; }

	int SaveChanges();
}

