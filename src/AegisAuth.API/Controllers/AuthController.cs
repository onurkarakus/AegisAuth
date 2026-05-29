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
}
