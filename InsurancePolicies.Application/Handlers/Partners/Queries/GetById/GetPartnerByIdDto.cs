using InsurancePolicies.Application.Handlers.Partners.Helpers.Enums;

namespace InsurancePolicies.Application.Handlers.Partners.Queries.GetById;

public class GetPartnerByIdDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PartnerNumber { get; set; } = string.Empty;
    public string? CroatianPIN { get; set; }
    public PartnerTypeId PartnerTypeId { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public string CreatedByUser { get; set; } = string.Empty;
    public bool IsForeign { get; set; }
    public string ExternalCode { get; set; } = string.Empty;
    public Gender Gender { get; set; }
}
