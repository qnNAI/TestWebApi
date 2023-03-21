using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Infrastructure.Persistence.Repositories; 

public static class PagedQueryHelper {

    /// <summary>
    /// Fetches page with page number <paramref name="pageNumber"/> with a page size set to <paramref name="pageSize"/>.
    /// Last page may contains 0 - <paramref name="pageSize"/> items. The page number <paramref name="pageNumber"/> is 0-based,
    /// i.e starts with 0. The method relies on the 'FETCH NEXT' and 'OFFSET' methods
    /// of the database engine provider.
    /// Note: When sorting with <paramref name="sortAscending"/> set to false, you will at the first page get the last items.
    /// The parameter <paramref name="orderByMember"/> specified which property member to sort the collection by. Use a lambda.
    /// </summary>
    /// <typeparam name="T">The type of ienumerable to return and strong type to return upon</typeparam>
    /// <param name="connection">IDbConnection instance</param>
    /// <param name="orderByMember">The property to order with</param>
    /// <param name="sql">The select clause sql to use as basis for the complete paging</param>
    /// <param name="pageNumber">The page index to fetch. 0-based (Starts with 0)</param>
    /// <param name="pageSize">The page size. Must be a positive number</param>
    /// <param name="sortAscending">Which direction to sort. True means ascending, false means descending</param>
    /// <returns></returns>
    public static async Task<IEnumerable<T>> GetPageAsync<T>(this IDbConnection connection, Expression<Func<T, object>> orderByMember,
        string sql, int pageNumber, int pageSize, bool sortAscending = true) {

        if(string.IsNullOrEmpty(sql) || pageNumber < 0 || pageSize <= 0) {
            throw new InvalidOperationException("Invalid sql query.");
        }

        int skip = Math.Max(0, (pageNumber)) * pageSize;
        if(!sql.Contains("order by", StringComparison.CurrentCultureIgnoreCase)) {
            string orderByMemberName = GetMemberName(orderByMember);
            sql += $" ORDER BY [{orderByMemberName}] {(sortAscending ? "ASC" : " DESC")} OFFSET @Skip ROWS FETCH NEXT @Next ROWS ONLY";
            return await connection.ParameterizedQueryAsync<T>(sql, new Dictionary<string, object> { { "@Skip", skip }, { "@Next", pageSize } });
        } else {
            sql += $" OFFSET @Skip ROWS FETCH NEXT @Next ROWS ONLY";
            return await connection.ParameterizedQueryAsync<T>(sql, new Dictionary<string, object> { { "@Skip", skip }, { "@Next", pageSize } });
        }

    }

    private static string GetMemberName<T>(Expression<Func<T, object>> expression) {
        switch(expression.Body) {
            case MemberExpression m:
                return m.Member.Name;
            case UnaryExpression u when u.Operand is MemberExpression m:
                return m.Member.Name;
            default:
                throw new NotImplementedException(expression.GetType().ToString());
        }
    }

    public static async Task<IEnumerable<T>> ParameterizedQueryAsync<T>(this IDbConnection connection, string sql,
        Dictionary<string, object> parametersDictionary) {

        if(string.IsNullOrEmpty(sql)) {
            throw new InvalidOperationException("Invalid sql query.");
        }

        foreach(var item in parametersDictionary) {
            if(!sql.Contains(item.Key)) {
                throw new ArgumentException($"Parameterized query failed.");
            }
        }

        var parameters = new DynamicParameters(parametersDictionary);
        return await connection.QueryAsync<T>(sql, parameters);
    }
}
