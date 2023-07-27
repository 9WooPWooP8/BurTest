using System.Net;

namespace BurTestTests;

using bur_test.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public class ApiWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
        });
    }
}


public class WellControllerTest : IClassFixture<ApiWebApplicationFactory<bur_test.Program>>
{
    readonly HttpClient _client;

    public WellControllerTest(ApiWebApplicationFactory<bur_test.Program> application)
    {
        _client = application.CreateClient();
        var scopeFactory = application.Services.GetService<IServiceScopeFactory>();

        using (var scope = scopeFactory.CreateScope())
        {
            var myDataContext = scope.ServiceProvider.GetService<BurDbContext>();

            //TODO: Extremely bad way to create data
            myDataContext.Telemetry.RemoveRange(myDataContext.Telemetry);
            myDataContext.Companies.RemoveRange(myDataContext.Companies);
            myDataContext.Wells.RemoveRange(myDataContext.Wells);
            myDataContext.TelemetryHistory.RemoveRange(myDataContext.TelemetryHistory);
            myDataContext.SaveChanges();

            myDataContext.Add(new Company { Id = 1, Name = "company 1" });
            myDataContext.Add(new Company { Id = 2, Name = "company 2" });
            myDataContext.Add(new Company { Id = 3, Name = "company 3" });
            myDataContext.Add(new Telemetry { Id = 1, DateTime = DateTime.Now, Depth = 1 });
            myDataContext.Add(new Telemetry { Id = 2, DateTime = DateTime.Now, Depth = 2 });
            myDataContext.Add(new Telemetry { Id = 3, DateTime = DateTime.Now, Depth = 3 });

            myDataContext.Add(new Well { Id = 1, Name = "well 1", TelemetryId = 1, CompanyId = 1, Active = 1 });
            myDataContext.Add(new Well { Id = 2, Name = "well 2", TelemetryId = 2, CompanyId = 2, Active = 0 });
            myDataContext.Add(new Well { Id = 3, Name = "well 3", TelemetryId = 3, CompanyId = 3, Active = 0 });
            myDataContext.SaveChanges();


        }
    }

    [Fact]
    public async Task GET_by_existing_id_returns_ok()
    {
        var response = await _client.GetAsync("/Well/1");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GET_by_nonexistent_id_returns_not_found()
    {
        var response = await _client.GetAsync("/Well/4");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GET_by_company_returns_ok()
    {
        var response = await _client.GetAsync("/Well/?companyName=comp");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GET_active_by_company_returns_ok()
    {
        var response = await _client.GetAsync("/Well/active/?companyName=comp");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GET_active_by_existing_id_returns_ok()
    {
        var response = await _client.GetAsync("/Well/active/1");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GET_active_by_nonexistent_id_returns_not_found()
    {
        var response = await _client.GetAsync("/Well/active/5");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GET_detailed_active_returns_ok()
    {
        var response = await _client.GetAsync("/Well/detailed/active");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GET_active_depth_progress_returns_ok()
    {
        var response = await _client.GetAsync("/Well/active/depthProgress");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GET_depth_progress_by_id_returns_ok()
    {
        var response = await _client.GetAsync("/Well/1/depthProgress");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
