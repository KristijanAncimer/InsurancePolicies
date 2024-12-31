using MediatR;

namespace InsurancePolicies.Application.Handlers.Partners.Queries.GetAll;

public class GetAllPartnersRequest : IRequest<IEnumerable<GetAllPartnersDto>>
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    private GetAllPartnersRequest(int pageSize, int pageNumber)
    {
        PageSize = pageSize;
        PageNumber = pageNumber;
    }
    public static GetAllPartnersRequest Create(int pageSize, int pageNumber) =>
        new (pageSize, pageNumber);
}
