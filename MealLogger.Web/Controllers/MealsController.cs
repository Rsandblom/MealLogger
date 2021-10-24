using MealLogger.Web.Models;
using MealLogger.Web.DataService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;


namespace MealLogger.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly ILogger<MealsController> _logger;

        public MealsController(IDataService dataService, ILogger<MealsController> logger)
        {
            _dataService = dataService;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult Get()
        {
            List<Meal> mealList = new List<Meal>();
            try
            {
                mealList = _dataService.GetMeals();
                if (mealList.Count != 0)
                {
                    _logger.LogInformation($"Returned Meallist from database");
                    return Ok(mealList.ToArray());
                }
                else
                {
                    _logger.LogInformation($"No Meals found in database");
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to return Meals: {ex}");
                return BadRequest($"Failed to return Meals");
            }            
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Meal meal = null;
            try
            {
                meal = _dataService.GetMealById(id);
                
                if (meal != null && meal.Id == id)
                {
                    _logger.LogInformation($"Returned Meal from database");
                    return Ok(meal);
                }
                else
                {
                    _logger.LogInformation($"Meal not found in database");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to return Meal: {ex}");
                return BadRequest($"Failed to return Meal");
            }
        }


        [HttpPost]
        public IActionResult Post(Meal meal)
        {
            try
            {
                int result = _dataService.CreateMeal(meal.TypeOfMeal, meal.MealDescription);
                if (result == 1)
                {
                    _logger.LogInformation($"Meal created in database");
                    return Created($"/api/meals", null);
                }
                else
                {
                    _logger.LogInformation($"Meal was not created.");
                    return Conflict();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create Meal: {ex}");
                return BadRequest($"Failed to create Meal");
            }

        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                int result = _dataService.DeleteMealById(id);

                if (result == 1)
                {
                    _logger.LogInformation($"Meal deleted from database");
                    return Ok("Meal deleted");
                }
                else
                {
                    _logger.LogInformation($"Meal not found in database. No meal deleted.");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to delete Meal: {ex}");
                return BadRequest($"Failed to delete Meal");
            }
        }
    }
}
