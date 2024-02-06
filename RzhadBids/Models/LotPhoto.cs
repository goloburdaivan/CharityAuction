namespace RzhadBids.Models
{
    public class LotPhoto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int LotId { get; set; }
        public Lot Lot { get; set; }
    }
}
