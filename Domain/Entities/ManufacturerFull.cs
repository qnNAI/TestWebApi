﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class ManufacturerFull : Manufacturer {

    public List<Car> Cars { get; set; } = new List<Car>();
}
