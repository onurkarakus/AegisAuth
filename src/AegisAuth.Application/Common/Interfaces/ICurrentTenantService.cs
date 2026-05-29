using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AegisAuth.Application.Common.Interfaces;

public interface ICurrentTenantService
{
    Guid? TenantId { get; }
}
