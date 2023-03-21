using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Contracts.Contexts;
using Application.Common.Contracts.Repositories;

namespace Infrastructure.Persistence.Repositories;

internal class CarRepository : ICarRepository {
    private readonly IDbContext _context;

    public CarRepository(IDbContext context) {
        this._context = context;
    }
}

