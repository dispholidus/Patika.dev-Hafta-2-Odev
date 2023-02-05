namespace RestaurantMenuApi.Model
{
    public class DbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            RestaurantMenuDbContext context = applicationBuilder.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<RestaurantMenuDbContext>();

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { CategoryName = "Starter" },
                    new Category { CategoryName = "Main" },
                    new Category { CategoryName = "Drinks" }
                    );
            }
            if (!context.RestaurantMenuItems.Any())
            {
                context.RestaurantMenuItems.AddRange(
                    new RestaurantMenuItem
                    {
                        RestaurantMenuItemName = "Roasted Stuffed Mushrooms",
                        Price = 25,
                        RestaurantMenuItemDescription = "Hearty comfort meets healthy eating in " +
                    "this delicious recipe. Plus it's kind to your wallet as well as your waistline.",
                        CategoryId = 1
                    },
                    new RestaurantMenuItem
                    {
                        RestaurantMenuItemName = "Creamy Penne Carbonara",
                        Price = 35,
                        RestaurantMenuItemDescription = "Budget-friendly, tasty and quick! Penne carbonara ticks all the boxes.",
                        CategoryId = 2
                    },
                    new RestaurantMenuItem
                    {
                        RestaurantMenuItemName = "Coca-Cola",
                        Price = 10,
                        RestaurantMenuItemDescription = "Coke just because.",
                        CategoryId = 3
                    }
                    );

            }
            context.SaveChanges();
        }
    }
}
