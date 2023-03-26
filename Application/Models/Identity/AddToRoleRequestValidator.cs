using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Models.Identity;

public class AddToRoleRequestValidator : AbstractValidator<AddToRoleRequest> {

	public AddToRoleRequestValidator() {
		RuleFor(r => r.Username).NotEmpty();
		RuleFor(r => r.RoleName).NotEmpty();
	}
}
