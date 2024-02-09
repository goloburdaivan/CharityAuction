namespace RzhadBids.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string Title { get; set; }   
        public List<Message> Messages { get; set; } = new List<Message>();
    }
}
