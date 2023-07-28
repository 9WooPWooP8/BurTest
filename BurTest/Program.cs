using BurTest.Domain.Interfaces;
using BurTest.Repository;
using BurTest.Data.Models;
using BurTest.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace BurTest;
public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSignalR(hubOptions =>
        {
            /* hubOptions.HandshakeTimeout = TimeSpan.FromMinutes(1); */
            /* hubOptions.EnableDetailedErrors = true; */
            /* hubOptions.KeepAliveInterval = TimeSpan.FromMinutes(10); */
            /* hubOptions.ClientTimeoutInterval = TimeSpan.FromMinutes(10); */
        });

        var folder = Environment.SpecialFolder.Startup;
        var path = Environment.GetFolderPath(folder);
        var dbPath = System.IO.Path.Join(path, "bur.db");

        builder.Services.AddDbContext<BurDbContext>(
			optionsBuilder => optionsBuilder.UseSqlite($"Data Source={dbPath}")
		);

        builder.Services.AddScoped<IWellService, WellService>();
        builder.Services.AddScoped<IWellRepository, WellRepository>();
        builder.Services.AddScoped<ITelemetryRepository, TelemetryRepository>();
        builder.Services.AddScoped<ITelemetryService, TelemetryService>();

        builder.Services.AddHostedService<DeactivateInactiveWellsBackgroundService>();

        builder.Services.AddAutoMapper(typeof(MappingProfile));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        /* app.UseHttpsRedirection(); */

		using (var scope = app.Services.CreateScope())
		{
			var services = scope.ServiceProvider;

			var context = services.GetRequiredService<BurDbContext>();    
			context.Database.Migrate();
		}

        app.UseAuthorization();

        app.MapControllers();

        app.MapHub<TelemetryUpdateHub>("/telemetryUpdates");
        /* app.MapHub<TelemetryUpdateHub>("/telemetryUpdates", */
        /* 		options => { options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.LongPolling; }); */

        app.Run();
    }
}
