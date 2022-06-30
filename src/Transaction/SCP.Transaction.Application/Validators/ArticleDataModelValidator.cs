using FluentValidation;
using SCP.Transaction.Application.Models;

namespace SCP.Transaction.Application.Validators
{
    public class ArticleDataModelValidator : AbstractValidator<ArticleDataModel>
    {
        public ArticleDataModelValidator()
        {
            var nameError = "Invalid or missing article name";
            RuleFor(x => x.Name)
                .NotNull()
                    .WithMessage(nameError)
                .NotEmpty()
                    .WithMessage(nameError)
                .MaximumLength(20)
                    .WithMessage(nameError);

            var eanError = "Invalid or missing EAN";
            RuleFor(x => x.EAN)
                .NotNull()
                    .WithMessage(eanError)
                .NotEmpty()
                    .WithMessage(eanError)
                .MaximumLength(10)
                    .WithMessage(eanError);

            var descriptionError = "Invalid or missing article description";
            RuleFor(x => x.Description)
                .NotNull()
                    .WithMessage(descriptionError)
                .NotEmpty()
                    .WithMessage(descriptionError)
                .MaximumLength(100)
                    .WithMessage(descriptionError);

            var categoryError = "Invalid or missing article category";
            RuleFor(x => x.ArticleCategory)
                .NotNull()
                    .WithMessage(categoryError)
                .NotEmpty()
                    .WithMessage(categoryError)
                .MaximumLength(20)
                    .WithMessage(categoryError);

            var colorError = "Invalid or missing article color";
            RuleFor(x => x.Color)
                .NotNull()
                    .WithMessage(colorError)
                .NotEmpty()
                    .WithMessage(colorError)
                .MaximumLength(10)
                    .WithMessage(colorError);

            var sizeError = "Invalid or missing article size";
            RuleFor(x => x.Size)
                .NotNull()
                    .WithMessage(sizeError)
                .NotEmpty()
                    .WithMessage(sizeError)
                .MaximumLength(10)
                    .WithMessage(sizeError);

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Invalid price");
        }
    }
}
