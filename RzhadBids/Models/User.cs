namespace RzhadBids.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public List<Bid> Bids { get; set; } = new List<Bid>();
    }
}
