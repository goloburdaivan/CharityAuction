using RzhadBids.Auth;
using RzhadBids.Models;

namespace RzhadBids.ViewModels
{
    public class LotViewModel
    {
        public Lot Lot { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
        public List<Message> Messages { get; set; } = new List<Message>();
    }
}
