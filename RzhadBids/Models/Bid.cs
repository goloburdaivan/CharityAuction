namespace RzhadBids.Models
{
    public class Bid
    {
        public int Id { get; set; }
        public decimal Sum { get; set; }
        public DateTime TimeStamp { get; set; }
        public int LotId { get; set; }
        public int UserId { get; set; }
        public Lot Lot { get; set; }
        public User User { get; set; }
    }
}
