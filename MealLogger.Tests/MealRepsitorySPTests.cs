using MealLogger.DataAccess.Data;
using MealLogger.DataAccess.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace MealLogger.Tests
{
    public class MealRepsitorySPTests
    {
        [Fact]
        public void MealRepositoryShouldReturnMealWithIdStoredProcedure()
        {
            Meal meal = new Meal();
            MealRepositorySP mealRepo = new MealRepositorySP();
            string dinnerId2 = "Chicken and rice";

            meal = mealRepo.GetMealById(2);

            Assert.Equal(dinnerId2, meal.MealDescription);
        }

        [Fact]
        public void MealRepositoryShouldReturnNullIfMealDoesntNotExist()
        {
            Meal meal = new Meal();
            MealRepositorySP mealRepo = new MealRepositorySP();

            meal = mealRepo.GetMealById(100);

            Assert.Null(meal);
        }

        [Fact]
        public void MealRepositoryShouldReturnListOfMealsStordeProcedure()
        {
            List<Meal> mealList = new List<Meal>();
            MealRepositorySP mealRepo = new MealRepositorySP();
            int count = 6;
            string dinnerId2 = "Chicken and rice";

            mealList = mealRepo.GetMeals();

            Assert.Equal(count, mealList.Count);
            Assert.Equal(dinnerId2, mealList[1].MealDescription);
        }

        [Fact]
        public void MealRepositoryShouldCreateMealStordeProcedure()
        {
            MealRepositorySP mealRepo = new MealRepositorySP();
            string typeOfMeal = "Breakfast";
            string mealDescription = "Scrambled eggs and bacon";
            int numberOfAffectedRows = 1;

            Assert.Equal(numberOfAffectedRows, mealRepo.CreateMeal(typeOfMeal, mealDescription));
        }


        [Fact]
        public void MealRepositoryShouldDeleteMealStordeProcedure()
        {
            MealRepositorySP mealRepo = new MealRepositorySP();
            int numberOfAffectedRows = 1;

            Assert.Equal(numberOfAffectedRows, mealRepo.DeleteMeal(12));
        }

    }
}
