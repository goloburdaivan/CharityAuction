using Microsoft.AspNetCore.Identity;
using RzhadBids.Models;

namespace RzhadBids.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public List<Lot> Lots { get; set; } 
    }
}
