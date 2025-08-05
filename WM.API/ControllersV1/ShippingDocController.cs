using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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
public class ShippingDocsController(IMediator mediator, ILogger<ShippingDocsController>logger) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        try
        {
            GetShippingDocBodiesListResponse ShippingDocsList = await _mediator.Send(new GetShippingDocBodiesListRequest());
            BaseResponse response = new(ShippingDocsList) { Success = true, Code = System.Net.HttpStatusCode.OK };
            return response.ToActionResult(this);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            BaseResponse baseResponse = new(new { ex.Message })
            {
                Code = HttpStatusCode.InternalServerError,
                Success = false
            };
            return baseResponse.ToActionResult(this);
        }
    }

    [Route("add")]
    [HttpPost]
    public async Task<ActionResult> Add([FromBody] ShippingDocBody inputBody)
    {
        try
        {
            var command = await _mediator.Send(new PostShippingDocRequest(inputBody));

            HttpStatusCode code = command.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
            BaseResponse response = new(null)
            {
                Success = command.Success,
                Code = code,
                Errors = command.Errors
            };
            return response.ToActionResult(this);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            BaseResponse baseResponse = new(new { ex.Message })
            {
                Code = HttpStatusCode.InternalServerError,
                Success = false
            };
            return baseResponse.ToActionResult(this);
        }
    }

    [Route("delete")]
    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] ShippingDocBody inputBody)
    {
        try
        {
            var command = await _mediator.Send(new DeleteShippingDocCommand(inputBody));

            HttpStatusCode code = command.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
            BaseResponse response = new(null)
            {
                Success = command.Success,
                Code = code,
                Errors = command.Errors
            };
            return response.ToActionResult(this);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            BaseResponse baseResponse = new(new { ex.Message })
            {
                Code = HttpStatusCode.InternalServerError,
                Success = false
            };
            return baseResponse.ToActionResult(this);
        }
    }
}
