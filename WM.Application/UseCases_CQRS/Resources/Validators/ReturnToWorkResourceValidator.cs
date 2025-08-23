using FluentValidation;
using WM.Application.Bodies;
using WM.Domain.Entities;
using WM.Domain.Models;

namespace WM.Application.UseCases_CQRS.Resources.Validators;

public class ReturnToWorkResourceValidator : AbstractValidator<ResourceBody>
{

    public ReturnToWorkResourceValidator(ResourceEntity? resource)
    {
        RuleFor(c => c.Name)
            .Custom((name, context) =>
            {
                if (resource is null)
                {
                    context.AddFailure(nameof(name), "Ресурс с таким названием не существует");
                }
                else if (resource.State != State.Archived)
                {
                    context.AddFailure(nameof(name), "Ресурс с таким названием не находится в архиве");
                }
            });
    }
}
