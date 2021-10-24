using MealLogger.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MealLogger.DataAccess.Data
{
    public interface IRepository
    {
        public Meal GetMealById(int id);
        public List<Meal> GetMeals();

        public int CreateMeal(string typeOfMeal, string mealDescription);

        public int DeleteMeal(int id);
    }
}
