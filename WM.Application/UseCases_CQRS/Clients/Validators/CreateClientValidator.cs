using FluentValidation;
using WM.Application.Bodies;
using WM.Application.Contracts;

namespace WM.Application.UseCases_CQRS.Clients.Validators;

public class CreateClientValidator : AbstractValidator<ClientBody>
{
    public CreateClientValidator(IClientRepository repository)
    {
        RuleFor(c => c.Name)
           .MustAsync(async (name, token) =>
           {
               return await repository.GetByName(name) is null;
           })
           .WithMessage("Клиент с именем создан ранее.")
           .NotEmpty().WithMessage("Поле Имя не должно быть путым")
           .NotNull()
           .MaximumLength(50).WithMessage("Максимальная длина 50 символов");


        RuleFor(c => c.Adress)
            .NotEmpty().WithMessage("Поле адрес не должно быть пустым")
            .NotNull();
    }


}
