using Microsoft.AspNetCore.SignalR;

namespace MySpaApp.Hubs;

public class TrainerHub : Hub
{
    public async Task NotifyChanged()
    {
        await Clients.All.SendAsync("TrainersUpdated");
    }
}