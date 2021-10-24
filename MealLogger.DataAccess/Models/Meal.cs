using System;
using System.Collections.Generic;
using System.Text;

namespace MealLogger.DataAccess.Models
{
    public class Meal
    {
        public int Id { get; set; }
        public string TypeOfMeal { get; set; }
        public string MealDescription { get; set; }

        public Meal()
        {

        }

        public Meal(int id, string typeOfMeal, string mealDecription)
        {
            Id = id;
            TypeOfMeal = typeOfMeal;
            MealDescription = mealDecription;
        }
    }
}
