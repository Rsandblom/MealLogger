using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MealLogger.Web.Models
{
    public class Meal
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Type of meal contains to many characters.")]
        public string TypeOfMeal { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage ="Description contains to many characters.")]
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
