using Microsoft.AspNetCore.Mvc;
using RestaurantMenuApi.Model;

namespace RestaurantMenuApi.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantMenuController : Controller
    {
        private readonly IRestaurantMenuItemRepository _restaurantMenuItemRepository;
        private readonly RestaurantMenuDbContext _restaurantMenuDbContext;
        private readonly ILogger _logger;

        public RestaurantMenuController(IRestaurantMenuItemRepository restaurantMenuItemRepository, RestaurantMenuDbContext restaurantMenuDbContext,
            ILogger<RestaurantMenuController> logger)
        {
            _restaurantMenuItemRepository = restaurantMenuItemRepository;
            _restaurantMenuDbContext = restaurantMenuDbContext;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetRestaurantMenu()
        {
            _logger.LogInformation($"RestaurantMenuItems requested and successfully sended.");
            return Ok(_restaurantMenuItemRepository.GetAllRestaurantMenuItem());
        }
        [HttpGet("id")]
        public IActionResult GetRestaurantMenuItemById(int id)
        {
            RestaurantMenuItem? restauranMenuItem = _restaurantMenuItemRepository.GetRestaurantMenuItemById(id);
            if (restauranMenuItem != null)
            {
                _logger.LogInformation($"RestaurantMenuItem with id = {id} requested and successfully sended.");
                return Ok(restauranMenuItem);
            }
            _logger.LogInformation($"RestaurantMenuItem with id = {id} requested but not found.");
            return NotFound();
        }
        [HttpPost]
        public IActionResult AddRestaurantMenuItem(string name, decimal price, string? restaurantMenuItemDescription, string? ingredients, int categoryId)
        {
            var newRestaurantMenuItem = new RestaurantMenuItem
            {
                RestaurantMenuItemName = name,
                Price = price,
                RestaurantMenuItemDescription = restaurantMenuItemDescription,
                Ingredients = ingredients,
                CategoryId = categoryId
            };

            if (_restaurantMenuDbContext.RestaurantMenuItems.FirstOrDefault(r => r.RestaurantMenuItemName.ToLower() == name.ToLower()) != null)
            {
                _logger.LogInformation($"RestaurantMenuItem with name = {newRestaurantMenuItem.RestaurantMenuItemName} already exist.");
                return BadRequest("Item already exist!");
            }

            _restaurantMenuItemRepository.AddRestaurantMenuItem(newRestaurantMenuItem);
            _logger.LogInformation($"RestaurantMenuItem with name = {newRestaurantMenuItem.RestaurantMenuItemName} successfully added.");
            return Ok(newRestaurantMenuItem);
        }

        [HttpDelete]
        public IActionResult DeleteRestaurantMenuItemById(int id)
        {
            if (_restaurantMenuItemRepository.DeleteRestaurantMenuItemById(id))
            {
                _logger.LogInformation($"RestaurantMenuItem with id = {id} successfully deleted.");
                return Ok($"Item with id = {id} deleted.");
            }
            _logger.LogInformation($"RestaurantMenuItem with id = {id} not found.");
            return NotFound();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateRestaurantMenuItemById(int id, [FromBody] RestaurantMenuItem restaurantMenuItem)
        {
            restaurantMenuItem.RestaurantMenuItemId = id;
            if (_restaurantMenuItemRepository.UpdateRestaurantMenuItemById(id, restaurantMenuItem))
            {
                _logger.LogInformation($"RestaurantMenuItem with id = {id} successfully updated.");
                return Ok($"Item with id = {id} updated.");
            }
            _logger.LogInformation($"RestaurantMenuItem with id = {id} not found.");
            return NotFound($"Item with id = {id} does not exist.");
        }
        [HttpPatch("{id}")]
        public IActionResult UpdateRestaurantItemCategoryById(int id, [FromBody] string newName)
        {
            if (_restaurantMenuItemRepository.UpdateRestaurantMenuItemNameById(id, newName))
            {
                _logger.LogInformation($"RestaurantMenuItem with id = {id} successfully patched.");
                return Ok($"Item with id = {id} name is changed to {newName}");
            }
            _logger.LogInformation($"RestaurantMenuItem with id = {id} not found.");
            return BadRequest($"Item with id = {id} does not exist.");
        }
    }
}
