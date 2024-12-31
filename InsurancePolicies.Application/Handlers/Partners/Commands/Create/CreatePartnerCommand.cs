using InsurancePolicies.Application.Handlers.Partners.Helpers.Enums;
using MediatR;
using System.Text.Json.Serialization;

namespace InsurancePolicies.Application.Handlers.Partners.Commands.Create;

public class CreatePartnerCommand : IRequest<CreatePartnerDto>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PartnerNumber { get; set; } = string.Empty;
    public string? CroatianPIN { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PartnerTypeId PartnerTypeId { get; set; }
    public string CreatedByUser { get; set; } = string.Empty;
    public bool IsForeign { get; set; }
    public string ExternalCode { get; set; } = string.Empty;
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Gender Gender { get; set; }
    private CreatePartnerCommand(string firstName, string lastName, string address, string partnerNumber, string? croatianPIN,
        PartnerTypeId partnerTypeId, string createdByUser, bool isForeign, string externalCode, Gender gender)
    {
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        PartnerNumber = partnerNumber;
        CroatianPIN = croatianPIN;
        PartnerTypeId = partnerTypeId;
        CreatedByUser = createdByUser;
        IsForeign = isForeign;
        ExternalCode = externalCode;
        Gender = gender;
    }
    public static CreatePartnerCommand Create(string firstName, string lastName, string address, string partnerNumber, string? croatianPIN,
        PartnerTypeId partnerTypeId, string createdByUser, bool isForeign, string externalCode, Gender gender) =>
        new(firstName, lastName, address, partnerNumber, croatianPIN, partnerTypeId, createdByUser, isForeign, externalCode, gender);
}