using FluentValidation;

namespace InsurancePolicies.Application.Handlers.Partners.Commands.Create;

public class CreatePartnerCommandValidator : AbstractValidator<CreatePartnerCommand>
{
    public CreatePartnerCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .MinimumLength(2)
            .WithMessage("First Name must be at least 2 characters long");
        RuleFor(x => x.LastName)
            .MinimumLength(2)
            .WithMessage("Last Name must be at least 2 characters long");
        RuleFor(x => x.PartnerNumber)
            .Length(20)
            .WithMessage("Partner number must be lenght of 20");
        RuleFor(x => x.PartnerNumber)
            .Must(value => !string.IsNullOrEmpty(value) && value.All(char.IsDigit))
            .WithMessage("Parnter number must be numeric");


    }
}
