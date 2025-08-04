using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Application.UseCases_CQRS.Resources.Validators;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Resources.Commands;

public class PostResourceRequest(ResourceBody body) : IRequest<BaseCommandResponse>
{
    public ResourceBody Body { get; set; } = body;
}


public class PostResourceRequestHandler(IResourceRepository repository, IMapper mapper) : IRequestHandler<PostResourceRequest, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(PostResourceRequest request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new PostResourceValidator(repository);
        var validationResult = await validator.ValidateAsync(request.Body, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Ресурс не добавлен";
            response.Errors = [.. validationResult.Errors.Select(q => q.ErrorMessage)];
        }
        else
        {
            var entity = mapper.Map<ResourceEntity>(request.Body);
            var addedEntity = await repository.Add(entity);
            response.Success = true;
            response.Message = "Ресурс добавлен успешно";
        }

        return response;
    }
}