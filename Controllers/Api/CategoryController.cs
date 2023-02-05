using Microsoft.AspNetCore.Mvc;
using RestaurantMenuApi.Model;

namespace RestaurantMenuApi.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly RestaurantMenuDbContext _restaurantMenuDbContext;
        private readonly ILogger _logger;

        public CategoryController(ICategoryRepository categoryRepository, RestaurantMenuDbContext restaurantMenuDbContext, ILogger<RestaurantMenuController> logger)
        {
            _categoryRepository = categoryRepository;
            _restaurantMenuDbContext = restaurantMenuDbContext;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            _logger.LogInformation($"Categories requested and successfully sended.");
            return Ok(_categoryRepository.GetCategories());
        }
        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            Category? category = _categoryRepository.GetCategoryById(id);
            if (category != null)
            {
                _logger.LogInformation($"Category with id = {id} requested and successfully sended.");
                return Ok(category);
            }
            _logger.LogInformation($"Category with id = {id} requested but not found.");
            return NotFound();
        }

        [HttpPost]
        public IActionResult AddCategory(string categoryName)
        {
            var category = new Category { CategoryName = categoryName };
            if (_restaurantMenuDbContext.Categories.FirstOrDefault(c => c.CategoryName.ToLower() == categoryName.ToLower()) != null)
            {
                _logger.LogInformation($"Category with name = {categoryName} already exist.");
                return BadRequest("Category Already Exist");
            }
            _categoryRepository.AddCategory(category);
            _logger.LogInformation($"Category with name = {categoryName} added.");
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategoryById(int id)
        {
            if (_categoryRepository.DeleteCategoryById(id))
            {
                _logger.LogInformation($"Category with id = {id} deleted.");
                return Ok($"Category with id = {id} deleted.");
            }
            _logger.LogInformation($"Category with id = {id} not found.");
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategoryById(int id, string categoryName)
        {
            Category category = new Category { CategoryId = id, CategoryName = categoryName };
            if (_categoryRepository.UpdateCategory(id, category))
            {
                _logger.LogInformation($"Category with id = {id} updated.");
                return Ok(category);
            }
            _logger.LogInformation($"Category with id = {id} not found.");
            return NotFound();
        }
    }
}
