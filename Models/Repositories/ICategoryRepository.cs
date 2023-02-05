namespace RestaurantMenuApi.Model
{
    public interface ICategoryRepository
    {
        public IEnumerable<Category> GetCategories();
        public Category? GetCategoryById(int categoryId);
        public void AddCategory(Category category);
        public bool DeleteCategoryById(int categoryId);
        public bool UpdateCategory(int id, Category category);

    }
}
