using Dapper;
using Domain.Core.Contracts.Repos;
using Domain.Core.Dtos.Product;
using Domain.Core.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infra.DataAccess.Repos.Dapper;

public class ProductDapperRepository : IProductDapperRepository
{
    private readonly IConfiguration _config;

    public ProductDapperRepository(IConfiguration configuration)
    {
        _config = configuration;
    }

    public async Task<List<ProductOutputDto>> GetNonAuctionsByCategoryId(CancellationToken cancellationToken,
                                                                 params int[] ids)
    {
        string WhereClause = string.Empty;
        for (int i = 0; i < ids.Length; i++)
        {
            if (ids.Length != 1 && i != ids.Length - 1)
                WhereClause += ids[i] + ",";
            else
                WhereClause += ids[i];
        }

        using var conn = new SqlConnection(_config.GetConnectionString("Default"));
        string sqlQuery = String.Format(Queries.GetNonAuctionProductsByCategoryId, WhereClause);
        var productdto = await conn.QueryAsync<ProductOutputDto>(sqlQuery, cancellationToken);
        return productdto.ToList();
    }

}
