using FluentValidation;
using SCP.Transaction.Application.Models;
using System.Text.RegularExpressions;

namespace SCP.Transaction.Application.Validators
{
    public class WorkstationDataModelValidator : AbstractValidator<WorkstationDataModel>
    {
        public WorkstationDataModelValidator()
        {
            var storeIdError = "Invalid store Id";
            RuleFor(x => x.StoreId)
                .NotEmpty()
                    .WithMessage(storeIdError)
                .NotNull()
                    .WithMessage(storeIdError)
                .Must(IsValidStoreId)
                    .WithMessage(storeIdError);

            var posIdError = "Invalid POS Id";
            RuleFor(x => x.POSId)
                .NotNull()
                    .WithMessage(posIdError)
                .GreaterThan(0)
                    .WithMessage(posIdError)
                .LessThan(999)
                    .WithMessage(posIdError);

            var cashierIdError = "Invalid cashier Id";
            RuleFor(x => x.CashierId)
                .NotEmpty()
                    .WithMessage(cashierIdError)
                .NotNull()
                    .WithMessage(cashierIdError)
                .Must(IsValidCashierId)
                    .WithMessage(cashierIdError);
        }

        private bool IsValidStoreId(string storeId)
        {
            return Regex.IsMatch(storeId, @"\d{4}");
        }

        private bool IsValidCashierId(string storeId)
        {
            return Regex.IsMatch(storeId, @"\d{3}");
        }
    }
}
