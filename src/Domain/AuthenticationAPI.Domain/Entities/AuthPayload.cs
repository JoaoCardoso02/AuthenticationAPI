using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AuthenticationAPI.Domain.Entities;

public class AuthPayload
{
	public int Id { get; set; }
	public string Email { get; set; }

	public AuthPayload(int id, string email)
	{
        Id = id;
		Email = email;
	}
}
