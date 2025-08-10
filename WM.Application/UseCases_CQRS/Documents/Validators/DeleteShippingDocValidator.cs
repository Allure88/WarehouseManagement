using FluentValidation;
using WM.Application.Bodies;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Documents.Validators;

public class DeleteShippingDocValidator : AbstractValidator<ShippingDocBody>
{
    public DeleteShippingDocValidator(ShippingDocEntity? entity)
    {
        RuleFor(c => c.Number)
            .Must((number) =>
            {
                return entity is not null;
            })
            .WithMessage("Документа с таким номером не существует");
    }
}
