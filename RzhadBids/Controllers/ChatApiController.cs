using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RzhadBids.Models;
using RzhadBids.Services;

namespace RzhadBids.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/chat/")]
    public class ChatApiController : BaseController
    {

        private readonly IHubContext<AuctionHub> hubContext;
        public ChatApiController(DatabaseContext databaseContext,
            IHubContext<AuctionHub> hubContext) : base(databaseContext)
        {
            this.hubContext = hubContext;
        }

        [HttpPost("sendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] Message message)
        {
            var currentUser = await UserManager.GetUserAsync(User);
            if (currentUser == null) {
                return BadRequest(new { Error = "Not Authorized" });
            }

            message.User = currentUser;
            await databaseContext.Messages.AddAsync(message);
            await databaseContext.SaveChangesAsync();
            await hubContext.Clients.All.SendAsync("ReceiveChatUpdate", message);

            return Ok(new { Status = "Message sent" });
        }
    }
}
