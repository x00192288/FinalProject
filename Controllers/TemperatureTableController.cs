using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using Xampp_Test2.Models;

namespace Xampp_Test2.Controllers
{
    public class TemperatureTableController : Controller
    {
        public IActionResult Index()
        {
            List<TemperatureTable> airs = new List<TemperatureTable>();
            // var builder = new ConfigurationBuilder();
            // builder.AddJsonFile("appsettings.json");
            //var configuration = builder.Build();
            //IConfiguration configuration = builder.Build();

            // string mainconn = configuration.GetConnectionString("MyConnection");

            //string mainconn = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;'
            MySqlConnection mySqlConnection = new MySqlConnection("server=localhost;database=db_air_api;uid=Arduino;password=x0192288!;SSLMode=none;");
            string sqlquery = "select * from air_api";

            MySqlCommand sqlcomm = new MySqlCommand(sqlquery, mySqlConnection);
            sqlcomm.CommandTimeout = 60;
            //MySqlDataReader sdr = sqlcomm.ExecuteReader();

            try
            {
                mySqlConnection.Open();
                MySqlDataReader myReader = sqlcomm.ExecuteReader();
                if (myReader.HasRows)
                {
                    Console.WriteLine("Your query generated results");
                    while (myReader.Read())
                    {
                        Console.WriteLine(myReader.GetString(0) + " - " + myReader.GetString(1));
                        //ViewBag.id = (myReader.GetString(0));
                        //ViewBag.value = (myReader.GetString(1));
                        string jsonString = myReader.GetString(1);
                        var temperature = JsonConvert.DeserializeObject<TemperatureTable>(jsonString);

                        if (temperature != null)
                        {
                            airs.Add(temperature);
                        }
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
            finally
            {
                mySqlConnection.Close();
            }

            return View(airs);
        }
    }
}
