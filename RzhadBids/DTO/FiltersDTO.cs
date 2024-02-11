namespace RzhadBids.DTO
{
    public class FiltersDTO
    {
        public string[] FilterCategories { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Actual { get; set; }
        public string? Unactual { get; set; }
        public string? MaxBids { get; set; }
        public string? SortDesc { get; set; }
        public string? SortAsc {  get; set; }
    }
}
