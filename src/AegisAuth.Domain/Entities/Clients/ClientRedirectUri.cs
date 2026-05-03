using System;
using AegisAuth.Domain.Entities.Base;

namespace AegisAuth.Domain.Entities.Clients;

public class ClientRedirectUri : BaseEntity
{
    public Guid ClientId { get; set; }
    public string Uri { get; set; }
    public bool IsWildcard { get; set; } = false;
    public Client Client { get; set; }
}
