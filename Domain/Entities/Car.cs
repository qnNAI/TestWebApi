using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities {

    public class Car : Entity<Guid> {

        public string Model { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime ManufDate { get; set; }
        public decimal Mileage { get; set; }
        public decimal Volume { get; set; }

        public Guid ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; } = null!;
    }
}
