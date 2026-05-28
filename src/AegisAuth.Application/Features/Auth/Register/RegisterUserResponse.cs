using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AegisAuth.Application.Features.Auth.Register;

public record RegisterUserResponse(string Email, string Message, string UserId);
