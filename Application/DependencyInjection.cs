﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection {

    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration) {

        return services;
    }
}

