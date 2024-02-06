using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RzhadBids.Models;

namespace RzhadBids.Configuration
{
    public class LotConfig : IEntityTypeConfiguration<Lot>
    {
        public void Configure(EntityTypeBuilder<Lot> builder)
        {
            builder.HasIndex(l => l.ChatId).IsUnique();
        }
    }
}
