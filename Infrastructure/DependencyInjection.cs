using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Contracts.Contexts;
using Application.Common.Contracts.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection {

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
        services.AddSingleton<IDbContext, DapperContext>();
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<IManufacturerRepository, ManufacturerRepository>();

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddDapperStores(opt => opt.ConnectionString = configuration.GetConnectionString("SqlConnection"));

        return services;
    }
}

