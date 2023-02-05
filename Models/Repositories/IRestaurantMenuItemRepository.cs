namespace RestaurantMenuApi.Model
{
    public interface IRestaurantMenuItemRepository
    {
        public void AddRestaurantMenuItem(RestaurantMenuItem restaurantMenuItem);
        public RestaurantMenuItem? GetRestaurantMenuItemById(int restauranMenuItemId);
        public IEnumerable<RestaurantMenuItem> GetAllRestaurantMenuItem();
        public bool DeleteRestaurantMenuItemById(int restaurantMenuItemId);
        public bool UpdateRestaurantMenuItemById(int RestaurantMenuItemId, RestaurantMenuItem newRestauranMenuItem);
        public bool UpdateRestaurantMenuItemNameById(int RestaurantMenuItemId, string newName);

    }
}
