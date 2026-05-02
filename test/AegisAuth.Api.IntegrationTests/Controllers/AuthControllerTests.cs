using AegisAuth.Api.IntegrationTests.Common;

namespace AegisAuth.Api.IntegrationTests.Controllers;

public class AuthControllerTests : BaseIntegrationTest
{
    public AuthControllerTests(AegisAuthWebAppFactory factory) : base(factory) { }

    // TODO: Add tests as Auth endpoints are implemented
    // Example:
    // [Fact]
    // public async Task Login_WithValidCredentials_ReturnsToken()
    // {
    //     var response = await Client.PostAsJsonAsync("/api/auth/login", new { Email = "...", Password = "..." });
    //     response.StatusCode.Should().Be(HttpStatusCode.OK);
    // }
}
