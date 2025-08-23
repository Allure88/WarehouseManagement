using FluentValidation;
using WM.Application.Bodies;
using WM.Domain.Entities;
using WM.Domain.Models;

namespace WM.Application.UseCases_CQRS.Units.Validators;

public class ReturnToWorkUnitValidator : AbstractValidator<UnitBody>
{

    public ReturnToWorkUnitValidator(UnitEntity? entity)
    {
        RuleFor(c => c.Name)
            .Custom((name, context) =>
            {
                if (entity is null)
                {
                    context.AddFailure(nameof(name), "Единицы измерения с таким названием не существует");
                }
                else if (entity.State != State.Archived)
                {
                    context.AddFailure(nameof(name), "Единицы измерения с таким названием не находится в архиве");
                }
            });
    }
}
