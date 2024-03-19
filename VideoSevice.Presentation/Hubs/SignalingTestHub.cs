using Microsoft.AspNetCore.SignalR;
using VideoService.Application.Abstractions;

namespace VideoSevice.Presentation.Hubs
{
    public class SignalingTestHub : Hub
    {
        private static readonly Dictionary<string, List<string>> ConnectedClients = new();
        private IAppointmentService _appointmentService;

        public SignalingTestHub(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public async Task<bool> Join(string channel)
        {
            if (!ConnectedClients.ContainsKey(channel))
            {
                ConnectedClients.Add(channel, new List<string>());
            }

            if (ConnectedClients[channel].Count() >= 2)
            {
                return false;
            }

            ConnectedClients[channel].Add(Context.ConnectionId);

            await Groups.AddToGroupAsync(Context.ConnectionId, channel);
            await Clients.OthersInGroup(channel).SendAsync("Join", Context.ConnectionId);

            return true;
        }
        public async Task Leave(string channel)
        {
            if (!IsClientInRoom(channel))
            {
                return;
            }

            ConnectedClients[channel].Remove(Context.ConnectionId);

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, channel);
            await Clients.OthersInGroup(channel).SendAsync("Leave", Context.ConnectionId);
        }

        // Used in rtc.razor/webrtcservice.cs
        public async Task SignalWebRtc(string channel, string type, string payload)
        {
            if (!IsClientInRoom(channel))
            {
                return;
            }

            await Clients.OthersInGroup(channel).SendAsync("SignalWebRtc", channel, type, payload);
        }

        // Used on index.razor
        public async Task Offer(string channel, string offer)
        {
            if (!IsClientInRoom(channel))
            {
                return;
            }

            await Clients.OthersInGroup(channel).SendAsync("ReceiveOffer", offer);
        }
        public async Task Answer(string channel, string answer)
        {
            if (!IsClientInRoom(channel))
            {
                return;
            }

            await Clients.OthersInGroup(channel).SendAsync("ReceiveAnswer", answer);
        }
        public async Task Candidate(string channel, string candidate)
        {
            if (!IsClientInRoom(channel))
            {
                return;
            }

            await Clients.OthersInGroup(channel).SendAsync("ReceiveCandidate", candidate);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var room = ConnectedClients.Where(k => k.Value.Any(v => v.Equals(Context.ConnectionId))).Select(k => k.Key).FirstOrDefault();

            if (room is not null)
            {
                await Leave(room);
            }
            await base.OnDisconnectedAsync(exception);
        }
        private bool IsClientInRoom(string roomName)
        {
            return ConnectedClients.ContainsKey(roomName) && ConnectedClients[roomName].Contains(Context.ConnectionId);
        }
    }
}
