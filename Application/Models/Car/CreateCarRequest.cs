using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Car;

public class CreateCarRequest {

    public string Model { get; set; } = null!;
    public decimal Price { get; set; }
    public DateTime ManufDate { get; set; }
    public decimal Mileage { get; set; }
    public decimal Volume { get; set; }

    public Guid ManufacturerId { get; set; }
}

