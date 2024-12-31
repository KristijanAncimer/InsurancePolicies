using Dapper;
using InsurancePolicies.Application.Handlers.Partners.Helpers.Enums;
using MediatR;
using System.Data;
using System.Text;

namespace InsurancePolicies.Application.Handlers.Partners.Queries.GetById;

public class GetPartnerByIdRequestHandler : IRequestHandler<GetPartnerByIdRequest ,GetPartnerByIdDto>
{
    private readonly IDbConnection _dbConnection;
    public GetPartnerByIdRequestHandler(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    public async Task<GetPartnerByIdDto> Handle(GetPartnerByIdRequest request, CancellationToken cancellationToken)
    {
        const string dbQuery = """
                            SELECT
                                pa.Id,
                                pa.FirstName,
                                pa.LastName,
                                pa.Address,
                                pa.PartnerNumber,
                                pa.CroatianPIN,
                                pa.PartnerTypeId,
                                pa.CreatedAtUtc,
                                pa.CreatedByUser,
                                pa.IsForeign,
                                pa.ExternalCode,
                                pa.Gender,
                                CASE
                                    WHEN COUNT(po.Id) > 5 OR SUM(po.PolicyAmount) > 5000 THEN 1
                                    ELSE 0
                                END AS Asteriks
                            FROM Partners pa
                            LEFT JOIN Policies po ON po.PartnerId = pa.Id
                            WHERE pa.Id = @requestId
                            GROUP BY 
                                pa.Id, pa.FirstName, pa.LastName, pa.Address, pa.PartnerNumber, pa.CroatianPIN, pa.PartnerTypeId, pa.CreatedAtUtc, pa.CreatedByUser, pa.IsForeign, pa.ExternalCode, pa.Gender
                            """;

        var sql = new StringBuilder();
        var parameters = new DynamicParameters();
        sql.Append(dbQuery);
        parameters.Add("@requestId", request.Id);

        var partner = await _dbConnection.QueryAsync<dynamic>(sql.ToString(), parameters);
        var a = new GetPartnerByIdDto
        {
            Id = partner.First().Id,
            FullName = $"{(partner.First().Asteriks == 1 ? "*" : "")}{partner.First().FirstName}, {partner.First().LastName}",
            Address = partner.First().Address,
            PartnerNumber = partner.First().PartnerNumber,
            CroatianPIN = partner.First().CroatianPIN,
            PartnerTypeId = (PartnerTypeId)partner.First().PartnerTypeId,
            CreatedAtUtc = DateTime.UtcNow,
            CreatedByUser = (string)partner.First().CreatedByUser,
            IsForeign = partner.First().IsForeign,
            ExternalCode = partner.First().ExternalCode,
            Gender = (Gender)partner.First().Gender,
        };
        return a;
    }
}
