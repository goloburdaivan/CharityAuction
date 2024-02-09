using RzhadBids.Auth;

namespace RzhadBids.Models
{
    public class Lot
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string? Description { get; set; } 
        public int CategoryId { get; set; }
        public int StartingPrice { get; set; } 
        public DateTime DateStart { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime DateEnd { get; set; }
        public int? ChatId { get; set; }
        public Chat? Chat { get; set; }
        public Category Category { get; set; }
        public List<LotPhoto> LotPhotos { get; set; } = new List<LotPhoto>();
        public List<Bid> Bids { get; set; } = new List<Bid>();
    }
}
