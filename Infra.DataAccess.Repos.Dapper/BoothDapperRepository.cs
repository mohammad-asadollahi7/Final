
using Dapper;
using Domain.Core.Contracts.Repos;
using Domain.Core.Dtos.Product;
using Domain.Core.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infra.DataAccess.Repos.Dapper;

public class BoothDapperRepository : IBoothDapperRepository
{
    private readonly IConfiguration _configuration;

    public BoothDapperRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<List<ProductInventoryDto>> GetInventoriesByBoothIds(List<int> boothIds,
                                            CancellationToken cancellationToken)
    {
        string WhereClause = string.Empty;
        for (int i = 0; i < boothIds.Count(); i++)
        {
            if (boothIds.Count() != 1 && i != boothIds.Count() - 1)
                WhereClause += boothIds[i] + ",";
            else
                WhereClause += boothIds[i];
        }

        using var conn = new SqlConnection(_configuration.GetConnectionString("Default"));
        string sqlQuery = String.Format(Queries.GetProductInventories, WhereClause);
        var productIvnentories = await conn.QueryAsync<ProductInventoryDto>(sqlQuery, cancellationToken);
        return productIvnentories.ToList();
    }


    public async Task UpdateMedal(List<UpdateMedalDto> updateMedalDtos,
                                    CancellationToken cancellationToken)
    {
        string sqlQuery = string.Empty;
        for (int i = 0; i < updateMedalDtos.Count(); i++)
        {
            sqlQuery += String.Format(Queries.UpdateMedal,
                                      updateMedalDtos[i].Wage,
                                      (int)updateMedalDtos[i].Medal,
                                      updateMedalDtos[i].BoothId) + ";";
        }

        using var conn = new SqlConnection(_configuration.GetConnectionString("Default"));
        await conn.ExecuteAsync(sqlQuery, cancellationToken);
    }
}
