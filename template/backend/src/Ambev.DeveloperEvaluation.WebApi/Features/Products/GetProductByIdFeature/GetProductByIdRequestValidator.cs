using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductByIdFeature;

public class GetProductByIdRequestValidator : AbstractValidator<GetProductByIdRequest>
{
    public GetProductByIdRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
