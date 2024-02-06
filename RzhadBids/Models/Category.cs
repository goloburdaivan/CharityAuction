namespace RzhadBids.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public List<Lot> Lots { get; set; } = new List<Lot>();
    }
}
