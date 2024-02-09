namespace RzhadBids.Filtering
{
    public class FilterStrategyFactory
    {
        public static IFilterStrategy GetFilterStrategy(FilterType type)
        {
            return type switch
            {
                FilterType.Category => new CategoryFilterStrategy(),
                FilterType.Price => new PriceFilterStrategy(),
                FilterType.Activity => new ActivityFilterStrategy(),
                FilterType.Date => new DateFilterStrategy(),
                _ => throw new ArgumentException("Unsupported filter type"),
            };
        }
    }
}
