using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WM.API.Models;
using WM.Application.Bodies;
using WM.Application.UseCases_CQRS.Documents.Commands;
using WM.Application.UseCases_CQRS.Documents.Queries;

namespace WM.API.ControllersV1;

[Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[ApiController]
public class ShippingDocsController(IMediator mediator, ILogger<ShippingDocsController> logger) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<BaseResponse> Get()
    {
        try
        {
            GetShippingDocBodiesListResponse ShippingDocsList = await _mediator.Send(new GetShippingDocBodiesListRequest());
            BaseResponse response = new(ShippingDocsList) { Success = true, Code = System.Net.HttpStatusCode.OK };
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            BaseResponse baseResponse = new(new { ex.Message })
            {
                Code = HttpStatusCode.InternalServerError,
                Success = false
            };
            return baseResponse;
        }
    }

    [Route("add")]
    [HttpPost]
    public async Task<BaseResponse> Add([FromBody] ShippingDocBody inputBody)
    {
        try
        {
            var command = await _mediator.Send(new CreateShippingDocRequest(inputBody));

            HttpStatusCode code = command.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
            BaseResponse response = new(null)
            {
                Success = command.Success,
                Code = code,
                Errors = command.Errors
            };
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            BaseResponse baseResponse = new(new { ex.Message })
            {
                Code = HttpStatusCode.InternalServerError,
                Success = false
            };
            return baseResponse;
        }
    }


    [Route("update")]
    [HttpPut]
    public async Task<BaseResponse> Update([FromBody] ShippingDocBody inputBody)
    {
        try
        {
            var command = await _mediator.Send(new UpdateShippingDocCommand(inputBody));

            HttpStatusCode code = command.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
            BaseResponse response = new(null)
            {
                Success = command.Success,
                Code = code,
                Errors = command.Errors
            };
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            BaseResponse baseResponse = new(new { ex.Message })
            {
                Code = HttpStatusCode.InternalServerError,
                Success = false
            };
            return baseResponse;
        }
    }

    [Route("sign")]
    [HttpPut]
    public async Task<BaseResponse> Update(string number)
    {
        try
        {
            var command = await _mediator.Send(new SignShippingDocCommand(number));

            HttpStatusCode code = command.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
            BaseResponse response = new(null)
            {
                Success = command.Success,
                Code = code,
                Errors = command.Errors
            };
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            BaseResponse baseResponse = new(new { ex.Message })
            {
                Code = HttpStatusCode.InternalServerError,
                Success = false
            };
            return baseResponse;
        }
    }

    [Route("revocate")]
    [HttpPut]
    public async Task<BaseResponse> Revocate(string number)
    {
        try
        {
            var command = await _mediator.Send(new RevocateShippingDocCommand(number));

            HttpStatusCode code = command.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
            BaseResponse response = new(null)
            {
                Success = command.Success,
                Code = code,
                Errors = command.Errors
            };
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            BaseResponse baseResponse = new(new { ex.Message })
            {
                Code = HttpStatusCode.InternalServerError,
                Success = false
            };
            return baseResponse;
        }
    }

    [Route("delete")]
    [HttpDelete]
    public async Task<BaseResponse> Delete([FromBody] ShippingDocBody inputBody)
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
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            BaseResponse baseResponse = new(new { ex.Message })
            {
                Code = HttpStatusCode.InternalServerError,
                Success = false
            };
            return baseResponse;
        }
    }
}
