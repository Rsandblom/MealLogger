using MealLogger.DataAccess.Data;
using MealLogger.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealLogger.Web.DataService
{
    public class MealDataService : IDataService
    {
        private MealRepositorySP _mealRepo;
        public MealDataService()
        {
            MealRepositorySP mealRepo = new MealRepositorySP();
            _mealRepo = mealRepo;
        }
        public int CreateMeal(string typeOfMeal, string mealDescription)
        {
            int result = _mealRepo.CreateMeal(typeOfMeal, mealDescription);
            return result;
        }

        public int DeleteMealById(int id)
        {
            int result = _mealRepo.DeleteMeal(id);
            return result;
        }

        public Meal GetMealById(int id)
        {
            var mfr = _mealRepo.GetMealById(id);
            if (mfr == null)
                return null;
            Meal meal = new Meal(mfr.Id, mfr.TypeOfMeal, mfr.MealDescription);
            return meal;
        }

        public List<Meal> GetMeals()
        {
            List<Meal> mealList = new List<Meal>();
            var mealListFromRepo = _mealRepo.GetMeals();
            foreach (var mfr in mealListFromRepo)
            {
                Meal meal = new Meal(mfr.Id, mfr.TypeOfMeal, mfr.MealDescription);
                mealList.Add(meal);
            }

            return mealList;
        }
    }
}
