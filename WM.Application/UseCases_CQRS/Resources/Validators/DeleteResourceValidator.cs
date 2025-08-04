using FluentValidation;
using WM.Application.Bodies;
using WM.Application.Contracts;

namespace WM.Application.UseCases_CQRS.Resources.Validators;

public class DeleteResourceValidator : AbstractValidator<ResourceBody>
{
    public DeleteResourceValidator(IResourceRepository repository)
    {
        RuleFor(c => c.Name)
            .Custom(async (name, context) =>
            {
                var resource = await repository.GetByNameWithDependents(name);
                if (resource is null)
                {
                    context.AddFailure(nameof(name), "Клиента с таким именем не существует");
                }
                else if (resource.AdmissionMovements.Count != 0)
                {
                    context.AddFailure(nameof(name), "Ресурс с таким названием используется в документах, и не может быть удален");
                }
                else if (resource.ShippingMovements.Count != 0)
                {
                    context.AddFailure(nameof(name), "Ресурс с таким названием используется в документах, и не может быть удален");
                }
                else if (resource.Balances.Count != 0)
                {
                    foreach (var balance in resource.Balances)
                    {
                        if (balance.Quantity > 1e-4)
                        {
                            context.AddFailure(nameof(name), "Ресурс с таким названием имеет положительный баланс, и не может быть удален");
                        }
                    }
                }
            });
        RuleFor(c => c.Adress)
            .NotEmpty().WithMessage("{ProperyName} не должно быть путым")
            .NotNull();
    }
}
