using RzhadBids.Auth;

namespace RzhadBids.Models
{
    public class Bid
    {
        public int Id { get; set; }
        public int Sum { get; set; }
        public DateTime TimeStamp { get; set; }
        public int LotId { get; set; }
        public Lot Lot { get; set; }
        public ApplicationUser User { get; set; }
    }
}
