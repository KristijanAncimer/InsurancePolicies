using InsurancePolicies.Application.Handlers.Partners.Queries.GetAll;
using MediatR;

namespace InsurancePolicies.Application.Handlers.Partners.Queries.GetById;

public class GetPartnerByIdRequest : IRequest<GetPartnerByIdDto>
{
    public int Id { get; set; }
    private GetPartnerByIdRequest(int id)
    {
        Id = id;
    }
    public static GetPartnerByIdRequest Create(int id) =>
        new(id);
}
