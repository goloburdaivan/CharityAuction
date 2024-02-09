using RzhadBids.Auth;
using RzhadBids.Models;

namespace RzhadBids.ViewModels
{
    public class ProfileViewModel
    {
        public ApplicationUser User { get; set; }
        public List<Lot> Lots { get; set; } = new List<Lot>();

    }
}
