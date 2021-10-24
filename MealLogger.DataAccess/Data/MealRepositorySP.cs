using log4net;
using log4net.Config;
using MealLogger.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text;

namespace MealLogger.DataAccess.Data
{
    public class MealRepositorySP : IRepository
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private string conStr;

        public MealRepositorySP()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetExecutingAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            conStr = DataConfig.GetDbConnection();
        }

        public Meal GetMealById(int id)
        {
            Meal meal = new Meal();

            try
            {
                using (SqlConnection connection = new SqlConnection(conStr))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("GetMealById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter param1 = new SqlParameter
                        {
                            ParameterName = "@Id",
                            SqlDbType = SqlDbType.Int,
                            Value = id,
                            Direction = ParameterDirection.Input
                        };
                        command.Parameters.Add(param1);

                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                meal.Id = Convert.ToInt32(dataReader["Id"]);
                                meal.TypeOfMeal = Convert.ToString(dataReader["TypeOfMeal"]);
                                meal.MealDescription = Convert.ToString(dataReader["MealDescription"]);
                            }

                        }
                    }
                    connection.Close();
                }

                if (meal.Id == id)
                    log.Info($"Meal with id {id} found in database");
                else
                    log.Info($"Meal with id {id} not found in database");
            }
            catch (Exception ex)
            {
                log.Error($"Somthing went wrong in the database: {ex.Message}");
            }
            
            return meal.Id == id ? meal : null;
        }

        public List<Meal> GetMeals()
        {

            DataTable returedData = null;
            List<Meal> mealsList = new List<Meal>();

            try
            {
                using (SqlConnection connection = new SqlConnection(conStr))
                {
                    connection.Open();
                    using (SqlCommand Command = new SqlCommand("GetMeals", connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(Command))
                        {
                            returedData = new DataTable();
                            dataAdapter.Fill(returedData);
                            foreach (DataRow row in returedData.Rows)
                            {
                                Meal meal = new Meal();
                                meal.Id = Convert.ToInt32(row["Id"]);
                                meal.TypeOfMeal = Convert.ToString(row["TypeOfMeal"]);
                                meal.MealDescription = Convert.ToString(row["MealDescription"]);
                                mealsList.Add(meal);
                            }
                        }
                    }
                    connection.Close();
                }

                if(mealsList.Count ==  0)
                    log.Info("No Meals found in database");
                else
                    log.Info("Meallist retrived from database");
            }
            catch (Exception ex)
            {
                log.Error($"Somthing went wrong in the database: {ex.Message}");
            }

            return mealsList;
        }

        public int CreateMeal(string typeOfMeal, string mealDescription)
        {
            int result = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(conStr))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("CreateMeal", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter tom = new SqlParameter
                        {
                            ParameterName = "@TypeOfMeal",
                            SqlDbType = SqlDbType.VarChar,
                            Value = typeOfMeal,
                            Direction = ParameterDirection.Input
                        };
                        command.Parameters.Add(tom);

                        SqlParameter md = new SqlParameter
                        {
                            ParameterName = "@MealDescription",
                            SqlDbType = SqlDbType.NVarChar,
                            Value = mealDescription,
                            Direction = ParameterDirection.Input
                        };
                        command.Parameters.Add(md);
                        result = command.ExecuteNonQuery();

                    }
                    connection.Close();
                }
                if (result == 1)
                    log.Info("Meal created");
                else
                    log.Info("No Meal created");
            }
            catch (Exception ex)
            {
                log.Error($"Somthing went wrong in the database: {ex.Message}");
            }

            return result;
        }

        public int DeleteMeal(int id)
        {
            int result = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(conStr))
                {

                    connection.Open();
                    using (SqlCommand command = new SqlCommand("DeleteMeal", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter delId = new SqlParameter
                        {
                            ParameterName = "@Id",
                            SqlDbType = SqlDbType.Int,
                            Value = id,
                            Direction = ParameterDirection.Input
                        };
                        command.Parameters.Add(delId);
                        result = command.ExecuteNonQuery();
                    }
                    connection.Close();
                }

                if (result == 1)
                    log.Info("Meal deleted");
                else
                    log.Info("No Meal deleted");
            }
            catch (Exception ex)
            {
                log.Error($"Somthing went wrong in the database: {ex.Message}");
            }
            
            return result;
        }
        
    }
}
