using System.Net;
using System.Net.Http.Json;
using System.Text;
using BurTest.Data.Models;
using BurTest.Domain.Dto;
using BurTest.Domain.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BurTestTests;

public class TelemetryApiTest : IClassFixture<ApiWebApplicationFactory<BurTest.Program>>
{
    readonly HttpClient _client;
	readonly ApiWebApplicationFactory<BurTest.Program> _application;
    readonly ITelemetryService _telemetryService;

    public TelemetryApiTest(ApiWebApplicationFactory<BurTest.Program> application)
    {
        _client = application.CreateClient();
		_application = application;
        var scopeFactory = _application.Services.GetService<IServiceScopeFactory>();

        using (var scope = scopeFactory.CreateScope())
        {
            var myDataContext = scope.ServiceProvider.GetService<BurDbContext>();

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
    public async Task POST_telemetry_returns_ok()
    {
        var telemetry = new List<TelemetryDto> {
			new TelemetryDto{
				Id = 1,
				DateTime = DateTime.Now,
				Depth = 5,
			},
			new TelemetryDto{
				Id = 2,
				DateTime = DateTime.Now,
				Depth = 5,
			},
			new TelemetryDto{
				Id = 3,
				DateTime = DateTime.Now,
				Depth = 5,
			}
		};


        var response = await _client.PostAsJsonAsync("/Telemetry", telemetry);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task POST_with_partialy_nonexistent_telemetry_returns_ok()
    {
        var telemetry = new List<TelemetryDto> {
			new TelemetryDto{
				Id = 1,
				DateTime = DateTime.Now,
				Depth = 5,
			},
			new TelemetryDto{
				DateTime = DateTime.Now,
				Depth = 5,
			},
			new TelemetryDto{
				DateTime = DateTime.Now,
				Depth = 5,
			}
		};


        var response = await _client.PostAsJsonAsync("/Telemetry", telemetry);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
