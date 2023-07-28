using BurTest.Domain.Interfaces;

namespace BurTest.Domain.Services;

public class DeactivateInactiveWellsBackgroundService : BackgroundService
{
	private readonly IServiceProvider _serviceProvider;

    public DeactivateInactiveWellsBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
		new Timer(DeactivateInactiveWells, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
		return Task.CompletedTask;
    }

	private async void DeactivateInactiveWells(object? state)
	{
		using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            IWellService wellService =
                scope.ServiceProvider.GetRequiredService<IWellService>();

            await wellService.DeactivateInactiveWells();
        }
	}
}
