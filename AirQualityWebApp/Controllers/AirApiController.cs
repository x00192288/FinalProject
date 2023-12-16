using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Xampp_Test2.Models;

namespace Xampp_Test2.Controllers
{
    public class AirApiController : Controller
    {
        private readonly IConfiguration _configuration;

        public AirApiController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: AirApiController
        public ActionResult Details()
        {
            List<AirApi> airs = new List<AirApi>();

            string connectionString = _configuration.GetConnectionString("TemperatureDbConnection");

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string sqlquery = "SELECT TOP 1 * FROM air_api ORDER BY api_id DESC";

                using (SqlCommand sqlCommand = new SqlCommand(sqlquery, sqlConnection))
                {
                    sqlCommand.CommandTimeout = 60;

                    try
                    {
                        sqlConnection.Open();
                        SqlDataReader myReader = sqlCommand.ExecuteReader();
                        if (myReader.HasRows)
                        {
                            Console.WriteLine("Your query generated results");
                            while (myReader.Read())
                            {
                                Console.WriteLine(myReader.GetValue(0).ToString() + " - " + myReader.GetValue(1).ToString());
                                ViewBag.id = (myReader.GetValue(0).ToString());
                                ViewBag.value = (myReader.GetValue(1).ToString());
                                ViewBag.city = (myReader.GetValue(2).ToString());
                                ViewBag.temperature = (myReader.GetValue(3).ToString());
                                ViewBag.inserttime = (myReader.GetValue(4).ToString());
                                ViewBag.date = (myReader.GetValue(5).ToString());
                            }
                        }
                        else
                        {
                            Console.WriteLine("Query successfully executed");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Query error " + e.Message);
                    }
                }
            }

            return View();
        }
    }
}
