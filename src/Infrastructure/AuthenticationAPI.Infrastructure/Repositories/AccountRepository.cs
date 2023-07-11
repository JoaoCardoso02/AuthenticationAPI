using AuthenticationAPI.Application.Common.Interfaces;
using AuthenticationAPI.Domain.Entities;
using AuthenticationAPI.Domain.ValueObjects;

namespace AuthenticationAPI.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
	private IApplicationDbContext _context;

	public AccountRepository(IApplicationDbContext context)
	{
		_context = context;
    }

	public Account? GetAccount(Email email)
	{
        return _context.Account.SingleOrDefault(account => account.Email.Value == email);
    }

    public int CreateAccount(Account account)
	{
        _context.Account.Add(account);
		_context.SaveChanges();

		return Convert.ToInt32(account.Id);
    }
}

