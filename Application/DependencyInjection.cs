using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Contracts.Services;
using Application.Common.Mappings;
using Application.Services;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection {

    public static IServiceCollection AddApplication(this IServiceCollection services) {
        MappingProfile.ApplyMappings();

        services.AddScoped<IManufacturerService, ManufacturerService>();
        services.AddScoped<ICarService, CarService>();
        services.AddScoped<IIdentityService, IdentityService>();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}

