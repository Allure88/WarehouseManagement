using FluentValidation;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Units.Validators;

public class ArchiveUnitValidator : AbstractValidator<UnitBody>
{
    public UnitEntity? Unit { get; private set; }
    public ArchiveUnitValidator(IUnitsRepository repository)
    {
        RuleFor(c => c.UnitDescription)
            .Custom(async (name, context) =>
            {
                Unit = await repository.GetByName(name);
                if (Unit is null)
                {
                    context.AddFailure(nameof(name), "Единицы измерения с таким названием не существует");
                }
                else if (Unit.State == State.Archived)
                {
                    context.AddFailure(nameof(name), "Единицы измерения с таким названием помещена в архив ранее");
                }
            });
    }
}
