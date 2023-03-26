using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Models.Identity;

public class CreateRoleRequestValidator : AbstractValidator<CreateRoleRequest> {

	public CreateRoleRequestValidator() {
		RuleFor(r => r.Name).NotEmpty();
	}

}
