using FluentValidation;
using WM.Application.Bodies;
using WM.Application.Contracts;

namespace WM.Application.UseCases_CQRS.Units.Validators;

public class UpdateUnitValidator : AbstractValidator<UnitBody>
{
    public UpdateUnitValidator(IUnitsRepository repository)
    {
        RuleFor(c => c.UnitDescription)
            .MustAsync(async (name, token) =>
            {
                return await repository.GetByName(name) is not null;
            })
            .WithMessage("Единица измерения с наименованием {ComparisonValue} не существует.")
            .NotEmpty().WithMessage("{ProperyName} не должно быть путым")
            .NotNull().WithMessage("{ProperyName} не должно быть путым")
            .MaximumLength(50).WithMessage("{ProperyName} максимальная длина 50 символов");

        RuleFor(c => c.State)
            .NotNull().WithMessage("{ProperyName} не должно быть путым");

    }
}