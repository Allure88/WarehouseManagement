using FluentValidation;
using WM.Application.Bodies;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Units.Validators;

public class DeleteUnitValidator : AbstractValidator<UnitBody>
{
    public DeleteUnitValidator(UnitEntity? unit)
    {
        RuleFor(c => c.Name)
           .Custom((name, context) =>
           {
               if (unit is null)
               {
                   context.AddFailure(nameof(name), "Единицы измерения с таким названием не существует");
               }
               else if (unit.AdmissionMovements.Count != 0)
               {
                   context.AddFailure(nameof(name), "Единица измерения с таким названием используется в документах, и не может быть удалена");
               }
               else if (unit.ShippingMovements.Count != 0)
               {
                   context.AddFailure(nameof(name), "Единица измерения с таким названием используется в документах, и не может быть удалена");
               }
               else if (unit.Balances.Count != 0)
               {
                   foreach (var balance in unit.Balances)
                   {
                       if (balance.Quantity > 1e-4)
                       {
                           context.AddFailure(nameof(name), "Единица измерения с таким названием используется для ресурсов положительным балансом, и не может быть удалена");
                       }
                   }
               }
           });

    }
}