using FluentValidation;
using WM.Application.Bodies;
using WM.Application.Contracts;

namespace WM.Application.UseCases_CQRS.Resources.Validators;

public class CreateResourceValidator:AbstractValidator<ResourceBody>
{
	public CreateResourceValidator(IResourceRepository repository)
	{
        RuleFor(c => c.Name)
             .MustAsync(async (name, token) =>
             {
                 return await repository.GetByName(name) is null;
             })
            .WithMessage("Ресурс с с таким наименованием создана ранее.")
           .NotEmpty().WithMessage("Поле наименование не должно быть пусСкутым")
           .NotNull()
           .MaximumLength(50).WithMessage("Максимальная длина поля наименование 50 символов");

        RuleFor(c => c.State)
                .NotEmpty().WithMessage("Состояние не должно быть пустым")
                .NotNull();
    }

}

