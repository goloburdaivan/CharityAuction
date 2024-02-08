using RzhadBids.Auth;

namespace RzhadBids.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }
        public int UserId { get; set; }
        public int ChatId { get; set; }
        public ApplicationUser User { get; set; } 
        public Chat Chat { get; set; }
    }
}
