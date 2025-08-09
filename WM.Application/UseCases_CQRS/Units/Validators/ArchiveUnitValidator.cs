using FluentValidation;
using WM.Application.Bodies;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Units.Validators;

public class ArchiveUnitValidator : AbstractValidator<UnitBody>
{
    public ArchiveUnitValidator(UnitEntity? entity)
    {
        RuleFor(c => c.Name)
            .Custom((name, context) =>
            {
                if (entity is null)
                {
                    context.AddFailure(nameof(name), "Единицы измерения с таким названием не существует");
                }
                else if (entity.State == State.Archived)
                {
                    context.AddFailure(nameof(name), "Единицы измерения с таким названием помещена в архив ранее");
                }
            });
    }
}
