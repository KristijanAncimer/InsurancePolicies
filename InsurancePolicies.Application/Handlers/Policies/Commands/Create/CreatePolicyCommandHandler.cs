using Dapper;
using MediatR;
using System.Data;
using System.Text;

namespace InsurancePolicies.Application.Handlers.Policies.Commands.Create;

public class CreatePolicyCommandHandler : IRequestHandler<CreatePolicyCommand, CreatePolicyDto>
{
    private readonly IDbConnection _dbConnection;
    public CreatePolicyCommandHandler(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    public async Task<CreatePolicyDto> Handle(CreatePolicyCommand command, CancellationToken cancellationToken)
    {
        const string dbQuery = @"
            INSERT INTO Policies (PolicyNumber, PolicyAmount, PartnerId)
            VALUES (@PolicyNumber, @PolicyAmount, @PartnerId);";
        var sql = new StringBuilder();
        var parameters = new DynamicParameters();

        sql.Append(dbQuery);
        parameters.Add("@PolicyNumber", command.PolicyNumber);
        parameters.Add("@PolicyAmount", command.PolicyAmount);
        parameters.Add("@PartnerId", command.PartnerId);

        await _dbConnection.ExecuteAsync(sql.ToString(), parameters);
        return new CreatePolicyDto { };
    }
}
