using BurTest.Domain.Dto;
using Microsoft.AspNetCore.SignalR;

namespace BurTest.Domain.Services;

public class TelemetryUpdateHub : Hub
{
	public async Task SendTelemetryUpdate(List<DetailedTelemetryDto> telemetryUpdate)
	{
		await Clients.All.SendAsync("RecieveTelemetryUpdates", telemetryUpdate);
	}
}
