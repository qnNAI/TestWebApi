using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities {

    public class Manufacturer : Entity<Guid> {

        public string Name { get; set; } = null!;

        public List<Car> Cars { get; set; } = new List<Car>();
    }
}
