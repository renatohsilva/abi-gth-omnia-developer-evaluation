using Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale
{
    public class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
    {
        public UpdateSaleValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Sale ID is required.");

            RuleFor(v => v.CustomerName)
                .NotEmpty().WithMessage("Customer name is required.")
                .MaximumLength(100).WithMessage("Customer name must not exceed 100 characters.");

            RuleFor(v => v.BranchName)
                .NotEmpty().WithMessage("Branch name is required.")
                .MaximumLength(100).WithMessage("Branch name must not exceed 100 characters.");

            RuleFor(v => v.Items)
                .NotEmpty().WithMessage("Sale must have at least one item.");

            RuleForEach(v => v.Items).SetValidator(new SaleItemDtoValidator());
        }
    }
}
