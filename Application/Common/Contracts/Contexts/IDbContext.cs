using System.Data;

namespace Application.Common.Contracts.Contexts;

public interface IDbContext {

    IDbConnection CreateConnection();
}

