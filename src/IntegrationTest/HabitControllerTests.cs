using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace IntegrationTest;

public class HabitControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public HabitControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ShouldBeUnauthorized()
    {
        var client = _factory.CreateClient();
        var response =await client.GetAsync($"Habit/{1}");
        var status = response.StatusCode;
        Assert.True((response.StatusCode == HttpStatusCode.Unauthorized));
    }
}