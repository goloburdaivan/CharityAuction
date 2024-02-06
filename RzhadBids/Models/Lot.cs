namespace RzhadBids.Models
{
    public class Lot
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string? Description { get; set; } 
        public int CategoryId { get; set; }
        public decimal StartingPrice { get; set; } 
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int? ChatId { get; set; }
        public Chat? Chat { get; set; }
        public List<LotPhoto> Photos { get; set; } = new List<LotPhoto>();
        public List<Bid> Bids { get; set; } = new List<Bid>();
    }
}
