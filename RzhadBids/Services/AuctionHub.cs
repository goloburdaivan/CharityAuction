using Microsoft.AspNetCore.SignalR;
using RzhadBids.Models;
using System.Security.Cryptography;
using System.Text.Json;

namespace RzhadBids.Services
{
    public class AuctionHub : Hub
    {
        public async Task UpdateBidHistory(Bid bid)
        {
            await Clients.All.SendAsync("ReceiveBidUpdate", JsonSerializer.Serialize(bid));
        }

        public async Task UpdateChat(Message message)
        {
            await Clients.All.SendAsync("ReceiveChatUpdate", JsonSerializer.Serialize(message));
        }
    }
}
