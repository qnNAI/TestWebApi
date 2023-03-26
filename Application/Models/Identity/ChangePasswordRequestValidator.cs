using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Models.Identity;

public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest> {

    public ChangePasswordRequestValidator() {

        RuleFor(r => r.Id).NotEmpty();
        RuleFor(r => r.OldPassword).NotEmpty();
        RuleFor(r => r.NewPassword).NotEmpty();
    }
}
