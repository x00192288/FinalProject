using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Xampp_Test2.Controllers
{
    public class VisualisationController : Controller
    {
        public IActionResult Histogram()
        {
            List<int> data = new List<int>();
            MySqlConnection mySqlConnection = new MySqlConnection("server=localhost;database=db_arduino;uid=Arduino;password=x0192288!;SSLMode=none;");
            string sqlquery = "select temp_value from tbl_temp";

            MySqlCommand sqlcomm = new MySqlCommand(sqlquery, mySqlConnection);
            sqlcomm.CommandTimeout = 60;
            //MySqlDataReader sdr = sqlcomm.ExecuteReader();

            try
            {
                mySqlConnection.Open();
                MySqlDataReader myReader = sqlcomm.ExecuteReader();
                if (myReader.HasRows)

                    if (myReader.HasRows)
                {
                    Console.WriteLine("Query returned results");

                    while (myReader.Read())
                    {
                        int value;
                        if (int.TryParse(myReader.GetString(0), out value))
                        {
                            data.Add(value);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Query successfully executed, but no results found");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Query error: " + e.Message);
            }
            finally
            {
                mySqlConnection.Close(); // Close the connection when done
            }

            int[] dataArray = data.ToArray(); // Convert the list to an array

            // Now you can use 'dataArray' which contains the 10 latest values as integers

            return View(dataArray);
        }

    }
}

