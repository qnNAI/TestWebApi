using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Models.Car;

public class CreateCarRequestValidator : AbstractValidator<CreateCarRequest> {

    public CreateCarRequestValidator() {

        RuleFor(c => c.Model).NotEmpty();
        RuleFor(c => c.Price).GreaterThan(0);
        RuleFor(c => c.ManufDate).NotEmpty();
        RuleFor(c => c.Volume).GreaterThan(0);
        RuleFor(c => c.ManufacturerId).NotEmpty();
    }
}

