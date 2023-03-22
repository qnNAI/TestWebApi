using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Car;

namespace Application.Models.Manufacturer;

public class ManufacturerDto {

    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<CarDto> Cars { get; set; } = null!;
}
