using Dapper;
using InsurancePolicies.Application.Handlers.Partners.Helpers.Enums;
using MediatR;
using System.Data;
using System.Text;

namespace InsurancePolicies.Application.Handlers.Partners.Queries.GetAll;

public class GetAllPartnersRequestHandler : IRequestHandler<GetAllPartnersRequest, IEnumerable<GetAllPartnersDto>>
{
    private readonly IDbConnection _dbConnection;
    public GetAllPartnersRequestHandler(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    public async Task<IEnumerable<GetAllPartnersDto>> Handle(GetAllPartnersRequest request, CancellationToken cancellationToken)
    {
        const string dbQuery = """
                            SELECT
                                pa.Id,
                                pa.FirstName,
                                pa.LastName,
                                pa.PartnerNumber,
                                pa.CroatianPIN,
                                pa.PartnerTypeId,
                                pa.CreatedAtUtc,
                                pa.IsForeign,
                                pa.Gender,
                                CASE
                                    WHEN COUNT(po.Id) > 5 OR SUM(po.PolicyAmount) > 5000 THEN 1
                                    ELSE 0
                                END AS Asteriks
                            FROM Partners pa
                            LEFT JOIN Policies po ON po.PartnerId = pa.Id
                            GROUP BY 
                                pa.Id, pa.FirstName, pa.LastName, pa.PartnerNumber, pa.CroatianPIN, pa.PartnerTypeId, pa.CreatedAtUtc,pa.IsForeign, pa.Gender
                            ORDER BY pa.CreatedAtUtc DESC
                                OFFSET @PageSize*(@PageNumber-1) ROWS FETCH NEXT @PageSize ROWS ONLY
                            """;

        var sql = new StringBuilder();
        var parameters = new DynamicParameters();
        sql.Append(dbQuery);
        parameters.Add("@PageSize", request.PageSize);
        parameters.Add("@PageNumber", request.PageNumber);

        var partners = await _dbConnection.QueryAsync<dynamic>(sql.ToString(), parameters);
        return partners.Select(x => new GetAllPartnersDto
        {
            Id = x.Id,
            FullName = $"{(x.Asteriks == 1 ? "*" : "")}{x.FirstName}, {x.LastName}",
            PartnerNumber = x.PartnerNumber,
            CroatianPIN = x.CroatianPIN,
            PartnerTypeId = (PartnerTypeId)x.PartnerTypeId,
            CreatedAtUtc = x.CreatedAtUtc,
            IsForeign = x.IsForeign,
            Gender = (Gender)x.Gender,
        });
    }
}
