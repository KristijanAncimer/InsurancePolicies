using FluentValidation;

namespace InsurancePolicies.Application.Handlers.Policies.Commands.Create;

public class CreatePolicyCommandValidator : AbstractValidator<CreatePolicyCommand>
{
    public CreatePolicyCommandValidator()
    {
        RuleFor(x => x.PolicyNumber)
            .MinimumLength(10)
            .MaximumLength(15)
            .WithMessage("Policy number must have between 10 and 15 numbers");
    }
}
