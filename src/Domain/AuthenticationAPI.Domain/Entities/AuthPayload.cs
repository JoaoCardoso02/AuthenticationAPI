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
	public Email Email { get; set; }

	public AuthPayload(int id, Email email)
	{
        Id = id;
		Email = email;
	}
}
