using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Application.UseCases_CQRS.Movements.Documents.Validators;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Movements.Documents.Commands;
public class PostAdmissionDocRequest(AdmissionDocBody body) : IRequest<BaseCommandResponse>
{
    public AdmissionDocBody Body { get; set; } = body;
}


public class PostAdmissionDocRequestHandler(IAdmissionDocRepository repository, IMapper mapper) : IRequestHandler<PostAdmissionDocRequest, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(PostAdmissionDocRequest request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new PostAdmissionDocValidator(repository);
        var validationResult = await validator.ValidateAsync(request.Body, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Баланс не изменен";
            response.Errors = [.. validationResult.Errors.Select(q => q.ErrorMessage)];
        }
        else
        {
            var entity = mapper.Map<AdmissionDocEntity>(request.Body);
            var addedEntity = await repository.Add(entity);
            response.Success = true;
            response.Message = "Баланс изменен успешно";
        }

        return response;
    }
}