using MealLogger.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealLogger.Web.DataService
{
    public interface IDataService
    {
        public Meal GetMealById(int id);
        public List<Meal> GetMeals();

        public int CreateMeal(string typeOfMeal, string mealDescription);

        public int DeleteMealById(int id);
    }
}
