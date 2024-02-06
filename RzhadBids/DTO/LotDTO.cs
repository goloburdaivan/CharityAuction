using System.ComponentModel.DataAnnotations;

namespace RzhadBids.DTO
{
    public class LotDTO
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public decimal StartingPrice { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int? ChatId { get; set; }
        public List<IFormFile> Photos { get; set; } = new List<IFormFile>();
    }
}
