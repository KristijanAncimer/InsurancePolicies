using Dapper;
using MediatR;
using System.Data;
using System.Text;

namespace InsurancePolicies.Application.Handlers.Partners.Commands.Create;

public class CreatePartnerCommandHandler : IRequestHandler<CreatePartnerCommand, CreatePartnerDto>
{
    private readonly IDbConnection _dbConnection;
    public CreatePartnerCommandHandler(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    public async Task<CreatePartnerDto> Handle(CreatePartnerCommand command, CancellationToken cancellationToken)
    {
        const string dbQuery = @"
            INSERT INTO Partners (FirstName, LastName, Address, PartnerNumber, CroatianPIN, PartnerTypeId, CreatedByUser, IsForeign, ExternalCode, Gender)
            OUTPUT INSERTED.Id
            VALUES (@FirstName, @LastName, @Address, @PartnerNumber, @CroatianPIN, @PartnerTypeId, @CreatedByUser, @IsForeign, @ExternalCode, @Gender);";

        var sql = new StringBuilder();
        var parameters = new DynamicParameters();

        sql.Append(dbQuery);
        parameters.Add("@FirstName", command.FirstName);
        parameters.Add("@LastName", command.LastName);
        parameters.Add("@Address", command.Address);
        parameters.Add("@PartnerNumber", command.PartnerNumber);
        parameters.Add("@CroatianPIN", command.CroatianPIN);
        parameters.Add("@PartnerTypeId", (int)command.PartnerTypeId);
        parameters.Add("@CreatedByUser", command.CreatedByUser);
        parameters.Add("@IsForeign", command.IsForeign);
        parameters.Add("@ExternalCode", command.ExternalCode);
        parameters.Add("@Gender", (int)command.Gender);

        var insertedId = await _dbConnection.QuerySingleAsync<int>(sql.ToString(), parameters);

        return new CreatePartnerDto { Id = insertedId };
    }
}
