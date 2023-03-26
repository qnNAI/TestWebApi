using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Models.Manufacturer;

public class CreateManufRequestValidator : AbstractValidator<CreateManufRequest> {

    public CreateManufRequestValidator() {
        RuleFor(x => x.Name).NotEmpty();
    }
}
