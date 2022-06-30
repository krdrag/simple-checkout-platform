using FluentValidation;
using SCP.Transaction.Application.Models;

namespace SCP.Transaction.Application.Validators
{
    public class PaymentModelValidator : AbstractValidator<PaymentModel>
    {
        public PaymentModelValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Invalid amount");

            RuleFor(x => x.PaymentMediaId)
                .GreaterThan(0)
                .WithMessage("Invalid paymetn media id");
        }
    }
}
