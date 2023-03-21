using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Contracts.Contexts;
using Application.Common.Contracts.Repositories;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection {

    public static IServiceCollection AddInfrastructure(this IServiceCollection services) {
        services.AddSingleton<IDbContext, DapperContext>();
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<IManufacturerRepository, ManufacturerRepository>();

        return services;
    }
}

