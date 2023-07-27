using bur_test.Domain.Dto;
using Microsoft.AspNetCore.SignalR;

namespace bur_test.Domain.Services;

public class TelemetryUpdateHub : Hub
{
	public async Task SendTelemetryUpdate(List<DetailedTelemetryDto> telemetryUpdate)
	{
		await Clients.All.SendAsync("RecieveTelemetryUpdates", telemetryUpdate);
	}
}
