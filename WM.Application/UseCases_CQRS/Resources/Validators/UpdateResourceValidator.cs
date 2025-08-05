using FluentValidation;
using WM.Application.Bodies;
using WM.Application.Contracts;

namespace WM.Application.UseCases_CQRS.Resources.Validators;

public class UpdateResourceValidator : AbstractValidator<ResourceBody>
{
    public UpdateResourceValidator(IResourceRepository repository)
    {
        RuleFor(c => c.Name)
             .MustAsync(async (name, token) =>
             {
                 return await repository.GetByName(name) is not null;
             })
            .WithMessage("Ресурс с наименованием {ComparisonValue} не существует.")
           .NotEmpty().WithMessage("{ProperyName} не должно быть путым")
           .NotNull()
           .MaximumLength(50).WithMessage("{ProperyName} максимальная длина 50 символов");

        RuleFor(c => c.State)
                .NotEmpty().WithMessage("{ProperyName} не должно быть путым")
                .NotNull();
    }



}

