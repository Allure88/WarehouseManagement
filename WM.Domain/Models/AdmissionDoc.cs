namespace WM.Domain.Models;

public class AdmissionDoc
{
    public static (AdmissionDoc? document, List<string> errors) GetInstance(
        AdmissionRes? inputAdmissionResource,
        string inputNumber,
        Predicate<string> checkNumberExist,
        DateTime inputDate,
        Func<string, Unit?> getUnitFromStorageByName,
        Func<string, Resource?> getresourceFromStorageByName)
    {
        List<string> errors = [];
        AdmissionDoc? document = null;

        if (inputAdmissionResource is not null)
        {
            Resource? resource = getresourceFromStorageByName?.Invoke(inputAdmissionResource.Resource.Name);
            Unit? unit = getUnitFromStorageByName?.Invoke(inputAdmissionResource.UnitOfMeasurement.Name);

            if (resource is null)
                errors.Add($"Ресурс с названием {inputAdmissionResource.Resource.Name} отсутствует.");
            else if (inputAdmissionResource.Resource.State == State.Archived)
                errors.Add("Архивный ресурс нельзя выбрать при создании документа поступления.");

            if (unit is null)
                errors.Add($"Единица измерения с названием {inputAdmissionResource.Resource.Name} отсутствует.");
            else if (inputAdmissionResource.UnitOfMeasurement.State == State.Archived)
                errors.Add("Архивную единиу измерения нельзя выбрать при создании документа поступления.");
        }
        //else
        //{
        //    errors.Add("Ресурс поступления отсутствует при создании документа поступления.");
        //}

        bool? exists = checkNumberExist?.Invoke(inputNumber);
        if (exists is null)
        {
            errors.Add("Невозможно проверить номер документа.");
        }
        else if (exists.Value)
        {
            errors.Add("Документ с таким номером существует.");
        }

        if (inputDate.Date != DateTime.Today.Date)
            errors.Add("Дата должна быть текущая.");

        if (errors.Count == 0)
            document = new(inputNumber, inputDate, inputAdmissionResource);

        return (document, errors);
    }


    private AdmissionDoc(string number, DateTime date, AdmissionRes? resource)
    {
        Number = number;
        Date = date;
        AdmissionRes = resource;
    }

    public string Number { get; } = string.Empty;
    public DateTime Date { get; }
    public AdmissionRes? AdmissionRes { get; }
}
