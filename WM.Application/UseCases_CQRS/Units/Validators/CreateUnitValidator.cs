using FluentValidation;
using WM.Application.Bodies;
using WM.Application.Contracts;

namespace WM.Application.UseCases_CQRS.Units.Validators;

public class CreateUnitValidator : AbstractValidator<UnitBody>
{
	public CreateUnitValidator(IUnitsRepository repository)
	{
        RuleFor(c => c.Name)
            .MustAsync(async (name, token) =>
            {
                return await repository.GetByName(name) is null;
            })
            .WithMessage("Единица измерения с таким наименованием создана ранее.")
            .NotEmpty().WithMessage("Поле наименование не должно быть пустым")
            .NotNull().WithMessage("Поле наименование не должно быть путым")
            .MaximumLength(50).WithMessage("Максимальная длина поля наименование 50 символов");

        RuleFor(c => c.State)
            .NotNull().WithMessage("Поле состояние не должно быть путым");

    }
}
