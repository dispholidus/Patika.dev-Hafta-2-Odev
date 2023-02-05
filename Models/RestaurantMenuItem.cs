namespace RestaurantMenuApi.Model
{
    public class RestaurantMenuItem
    {
        public int RestaurantMenuItemId { get; set; }
        public string RestaurantMenuItemName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? RestaurantMenuItemDescription { get; set; }
        public string? Ingredients { get; set; }
        public int CategoryId { get; set; }

    }
}
