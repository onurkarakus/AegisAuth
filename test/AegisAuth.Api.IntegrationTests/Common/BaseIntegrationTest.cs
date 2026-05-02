using AegisAuth.Api.IntegrationTests.Common;

namespace AegisAuth.Api.IntegrationTests.Common;

public abstract class BaseIntegrationTest : IClassFixture<AegisAuthWebAppFactory>, IDisposable
{
    protected readonly HttpClient Client;
    private readonly AegisAuthWebAppFactory _factory;

    protected BaseIntegrationTest(AegisAuthWebAppFactory factory)
    {
        _factory = factory;
        Client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    public void Dispose()
    {
        Client.Dispose();
    }
}
