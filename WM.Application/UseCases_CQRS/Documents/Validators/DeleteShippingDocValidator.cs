using FluentValidation;
using WM.Application.Bodies;
using WM.Application.Contracts;

namespace WM.Application.UseCases_CQRS.Documents.Validators;

public class DeleteShippingDocValidator : AbstractValidator<ShippingDocBody>
{
    public DeleteShippingDocValidator(IShippingDocRepository repository)
    {
        RuleFor(c => c.Number)
            .MustAsync(async (number, token) =>
            {
                return await repository.GetByNumber(number) is not null;
            })
            .WithMessage("Документа с таким номером не существует");
    }
}
