using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WM.API.Models;
using WM.API.Utils;
using WM.Application.Bodies;
using WM.Application.UseCases_CQRS.Movements.Documents.Commands;
using WM.Application.UseCases_CQRS.Movements.Documents.Queries;

namespace WM.API.ControllersV1;

[Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[ApiController]
public class ShippingResController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        try
        {
            GetShippingResBodiesListResponse ShippingRessList = await _mediator.Send(new GetShippingResBodiesListRequest());
            BaseResponse response = new(ShippingRessList) { Success = true, Code = System.Net.HttpStatusCode.OK };
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
    public async Task<ActionResult> Add([FromBody] ShippingResBody inputBody)
    {
        try
        {
            var command = await _mediator.Send(new PostShippingResRequest(inputBody));

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
