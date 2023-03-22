using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Models.Manufacturer {

    public class UpdateManufRequestValidator : AbstractValidator<UpdateManufRequest> {

        public UpdateManufRequestValidator() {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
