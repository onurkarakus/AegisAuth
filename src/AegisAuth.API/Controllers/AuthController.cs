using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AegisAuth.Application.Extensions;
using AegisAuth.Application.Features.Auth.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AegisAuth.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ISender _sender;
    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("register")]
    public async Task<IResult> Register([FromBody] RegisterUserCommand command)
    {
        var result = await _sender.Send(command);

        return result.MapResult(
            onSuccess: response => Results.Ok(response),
            onFailure: result => Results.Problem(result.ToProblemDetails())
        );
    }

    [HttpPost("connect/token")]
    public async Task<IResult> GetToken([FromForm] string grant_type, [FromForm] string client_id, [FromForm] string client_secret)
    {
        var command = new Application.Features.OAuth.Token.GenerateTokenCommand
        (
            GrantType: grant_type,
            ClientId: client_id,
            ClientSecret: client_secret
        );

        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            string oauthErrorCode = result.Error.Code == "AUTH_007" ? "invalid_client" : "unsupported_grant_type";

            return Results.Json(new
            {
                error = oauthErrorCode,
                error_description = result.Error.Description
            }, statusCode: StatusCodes.Status401Unauthorized);
        }

        return Results.Ok(result.Value);
    }
}
