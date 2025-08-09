using FluentValidation;
using WM.Application.Bodies;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Resources.Validators;

public class DeleteResourceValidator : AbstractValidator<ResourceBody>
{
    public DeleteResourceValidator(ResourceEntity? resource)
    {
        RuleFor(c => c.Name)
            .Custom((name, context) =>
            {
                if (resource is null)
                {
                    context.AddFailure(nameof(name), "Ресурс с таким именем не существует");
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
    }
}
