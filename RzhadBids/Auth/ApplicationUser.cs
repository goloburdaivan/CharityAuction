using Microsoft.AspNetCore.Identity;
using RzhadBids.Models;
using System.Text.Json.Serialization;

namespace RzhadBids.Auth
{
    public class ApplicationUser : IdentityUser
    {

        public string Name { get; set; }
        public string Surname { get; set; } 

        [JsonIgnore]
        public List<Lot> Lots { get; set; }
        [JsonIgnore]
        public override string? PasswordHash { get => base.PasswordHash; set => base.PasswordHash = value; }
        [JsonIgnore]
        public override string? PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }
        [JsonIgnore]
        public override string? ConcurrencyStamp { get => base.ConcurrencyStamp; set => base.ConcurrencyStamp = value; }
        [JsonIgnore]
        public override string? SecurityStamp { get => base.SecurityStamp; set => base.SecurityStamp = value; }
        [JsonIgnore]
        public override string? NormalizedEmail { get => base.NormalizedEmail; set => base.NormalizedEmail = value; }
        [JsonIgnore]
        public override string? NormalizedUserName { get => base.NormalizedUserName; set => base.NormalizedUserName = value; }  
    }
}
