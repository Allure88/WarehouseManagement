using FluentValidation;
using WM.Application.Bodies;
using WM.Application.Contracts;

namespace WM.Application.UseCases_CQRS.Resources.Validators;

public class PostResourceValidator:AbstractValidator<ResourceBody>
{
	public PostResourceValidator(IResourceRepository repository)
	{
        RuleFor(c => c.Name)
             .MustAsync(async (name, token) =>
             {
                 return await repository.GetByName(name) is null;
             })
            .WithMessage("Ресурс с наименованием {ComparisonValue} создана ранее.")
           .NotEmpty().WithMessage("{ProperyName} не должно быть путым")
           .NotNull()
           .MaximumLength(50).WithMessage("{ProperyName} максимальная длина 50 символов");

        RuleFor(c => c.State)
                .NotEmpty().WithMessage("{ProperyName} не должно быть путым")
                .NotNull();
    }


   
}
