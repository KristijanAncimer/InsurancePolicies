using MediatR;

namespace InsurancePolicies.Application.Handlers.Policies.Commands.Create;

public class CreatePolicyCommand : IRequest<CreatePolicyDto>
{
    public string PolicyNumber { get; set; } = string.Empty;
    public decimal PolicyAmount { get; set; }
    public int PartnerId { get; set; }
    private CreatePolicyCommand(string policyNumber, decimal policyAmount, int partnerId)
    {
        PolicyNumber = policyNumber;
        PolicyAmount = policyAmount;
        PartnerId = partnerId;
    }
    public static CreatePolicyCommand Create(string policyNumber, decimal policyAmount, int partnerId) =>
        new(policyNumber, policyAmount, partnerId);
}
