using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Models.Car;

public class UpdateCarRequestValidator : AbstractValidator<UpdateCarRequest> {

	public UpdateCarRequestValidator() {

        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Model).NotEmpty();
        RuleFor(c => c.Price).GreaterThan(0);
        RuleFor(c => c.ManufDate).NotEmpty();
        RuleFor(c => c.Volume).GreaterThan(0);
    }
}
