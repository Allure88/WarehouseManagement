using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WM.API.Models;
using WM.API.Utils;
using WM.Application.Bodies;
using WM.Application.UseCases_CQRS.Resources.Commands;
using WM.Application.UseCases_CQRS.Resources.Queries;

namespace WM.API.ControllersV1;

[Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[ApiController]
public class ResourcesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        try
        {
            GetResourceBodiesListResponse ResourcesList = await _mediator.Send(new GetResourceBodiesListRequest());
            BaseResponse response = new(ResourcesList) { Success = true, Code = System.Net.HttpStatusCode.OK };
            return response.ToActionResult(this);
        }
        catch (Exception ex)
        {
            string messageToUser = "";
            BaseResponse baseResponse = new(new { ex.Message })
            {
                Code = System.Net.HttpStatusCode.InternalServerError,
                Message = messageToUser,
                Success = false
            };
            return baseResponse.ToActionResult(this);
        }
    }

    [Route("add")]
    [HttpPost]
    public async Task<ActionResult> Add([FromBody] ResourceBody inputBody)
    {
        try
        {
            var command = await _mediator.Send(new PostResourceRequest(inputBody));

            BaseResponse response = new(command.Entity)
            {
                Success = true,
                Code = System.Net.HttpStatusCode.OK
            };
            return response.ToActionResult(this);
        }
        catch (Exception ex)
        {
            string messageToUser = "";
            BaseResponse baseResponse = new(new { ex.Message })
            {
                Code = System.Net.HttpStatusCode.InternalServerError,
                Message = messageToUser,
                Success = false
            };
            return baseResponse.ToActionResult(this);
        }
    }
}
