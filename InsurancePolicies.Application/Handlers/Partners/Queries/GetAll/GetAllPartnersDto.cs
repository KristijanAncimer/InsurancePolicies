using InsurancePolicies.Application.Handlers.Partners.Helpers.Enums;

namespace InsurancePolicies.Application.Handlers.Partners.Queries.GetAll;

public class GetAllPartnersDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string PartnerNumber { get; set; } = string.Empty;
    public string? CroatianPIN { get; set; }
    public PartnerTypeId PartnerTypeId { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public bool IsForeign { get; set; }
    public Gender Gender { get; set; }
}
