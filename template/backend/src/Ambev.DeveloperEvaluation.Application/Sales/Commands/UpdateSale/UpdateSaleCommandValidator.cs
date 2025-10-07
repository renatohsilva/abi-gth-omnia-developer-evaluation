using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;

public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
{
    public UpdateSaleCommandValidator()
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

        RuleForEach(v => v.Items).SetValidator(new UpdateSaleItemValidator());
    }
}
public class UpdateSaleItemValidator : AbstractValidator<UpdateSaleItemCommand>
{
    public UpdateSaleItemValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("Sale Item ID is required.");

        RuleFor(i => i.ProductId)
            .NotEmpty().WithMessage("Product ID is required.");

        RuleFor(i => i.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.")
            .LessThanOrEqualTo(20).WithMessage("Quantity must not exceed 20.");

        RuleFor(i => i.UnitPrice)
            .GreaterThan(0).WithMessage("Unit price must be greater than 0.");

        RuleFor(i => i.Discount)
            .InclusiveBetween(0, 1).WithMessage("Discount must be between 0 and 1.")
            .Must(Be_zero_if_quantity_is_less_than_4)
            .WithMessage("Discount is not allowed for quantities less than 4.");
    }

    private bool Be_zero_if_quantity_is_less_than_4(UpdateSaleItemCommand item, decimal discount)
    {
        if (item.Quantity < 4)
        {
            return discount == 0;
        }
        return true;
    }
}

