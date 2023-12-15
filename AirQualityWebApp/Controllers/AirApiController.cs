using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xampp_Test2.Models;
using MySql.Data.MySqlClient;
using System.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Xampp_Test2.Controllers
{
    public class AirApiController : Controller
    {
        // GET: AirApiController
        public ActionResult Details()
        {
            List<AirApi> airs = new List<AirApi>();
            // var builder = new ConfigurationBuilder();
            // builder.AddJsonFile("appsettings.json");
            //var configuration = builder.Build();
            //IConfiguration configuration = builder.Build();

            // string mainconn = configuration.GetConnectionString("MyConnection");

            //string mainconn = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;'
            MySqlConnection mySqlConnection = new MySqlConnection("server=localhost;database=db_air_api;uid=Arduino;password=x0192288!;SSLMode=none;");
            string sqlquery = "SELECT * FROM air_api ORDER BY api_id DESC LIMIT 1";

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
                        ViewBag.id = (myReader.GetString(0));
                        ViewBag.value = (myReader.GetString(1));
                        ViewBag.city = (myReader.GetString(2));
                        ViewBag.temperature = (myReader.GetString(3));
                        ViewBag.inserttime = (myReader.GetString(4));
                        ViewBag.date = (myReader.GetString(5));





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

            return View();
        }

        
    }
}