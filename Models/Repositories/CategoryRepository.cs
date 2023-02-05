namespace RestaurantMenuApi.Model
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly RestaurantMenuDbContext _restaurantMenuDbContext;

        public CategoryRepository(RestaurantMenuDbContext restaurantMenuDbContext)
        {
            _restaurantMenuDbContext = restaurantMenuDbContext;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _restaurantMenuDbContext.Categories.ToList();
        }

        public Category? GetCategoryById(int categoryId)
        {
            return _restaurantMenuDbContext.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
        }

        public void AddCategory(Category category)
        {
            _restaurantMenuDbContext.Add(category);
            _restaurantMenuDbContext.SaveChanges();
        }

        public bool DeleteCategoryById(int categoryId)
        {
            Category? category = _restaurantMenuDbContext.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (category != null)
            {
                _restaurantMenuDbContext.Remove(category);
                _restaurantMenuDbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateCategory(int categoryId, Category newCategory)
        {
            Category? categoryToUpdate = _restaurantMenuDbContext.Categories.FirstOrDefault(r => r.CategoryId == categoryId);
            if (categoryToUpdate != null)
            {
                categoryToUpdate = newCategory;
                _restaurantMenuDbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
