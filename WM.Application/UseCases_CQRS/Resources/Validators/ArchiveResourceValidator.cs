using FluentValidation;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Resources.Validators;

public class ArchiveResourceValidator : AbstractValidator<ResourceBody>
{
    public ResourceEntity? Resource { get; private set; }
    public ArchiveResourceValidator(IResourceRepository repository)
    {
        RuleFor(c => c.Name)
            .Custom(async (name, context) =>
            {
                Resource = await repository.GetByName(name);
                if (Resource is null)
                {
                    context.AddFailure(nameof(name), "Ресурс с таким названием не существует");
                }
                else if (Resource.State == State.Archived)
                {
                    context.AddFailure(nameof(name), "Ресурс с таким названием помещен в архив ранее");
                }
            });
    }
}
