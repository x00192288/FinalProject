using Xampp_Test2.Models;
using MySql.Data.MySqlClient;
using System.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Xampp_Test2.Controllers
{

    public interface IDbConnectionWrapper : IDisposable
    {
        void Open();
        void Close();
        MySqlCommand CreateCommand();
    }

    public class MySqlConnectionWrapper : IDbConnectionWrapper
    {
        private readonly MySqlConnection _mySqlConnection;

        public MySqlConnectionWrapper(string connectionString)
        {
            _mySqlConnection = new MySqlConnection(connectionString);
        }

        public void Open()
        {
            _mySqlConnection.Open();
        }

        public void Close()
        {
            _mySqlConnection.Close();
        }

        public MySqlCommand CreateCommand()
        {
            return _mySqlConnection.CreateCommand();
        }

        public void Dispose()
        {
            _mySqlConnection.Dispose();
        }
    }

    public class TemperatureController : Controller
    {
        private readonly IDbConnectionWrapper _mySqlConnection;

        public TemperatureController([FromServices] IDbConnectionWrapper temperatureDbConnection)
        {
            _mySqlConnection = temperatureDbConnection ?? throw new ArgumentNullException(nameof(temperatureDbConnection));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose of managed resources here
                _mySqlConnection?.Dispose();
            }

            base.Dispose(disposing);
        }


        // GET: TemperatureController
        public ActionResult Details()
        {
            List<Temperature> temps = new List<Temperature>();
            // var builder = new ConfigurationBuilder();
            // builder.AddJsonFile("appsettings.json");
            //var configuration = builder.Build();
            //IConfiguration configuration = builder.Build();

            // string mainconn = configuration.GetConnectionString("MyConnection");

            //string mainconn = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;'
            // MySqlConnection mySqlConnection = new MySqlConnection("server=localhost;database=db_arduino;uid=Arduino;password=x0192288!;SSLMode=none;");
            string sqlquery = "select * from tbl_temp";

            MySqlCommand sqlcomm = _mySqlConnection.CreateCommand();
            sqlcomm.CommandText = sqlquery;

            sqlcomm.CommandTimeout = 60;
            //MySqlDataReader sdr = sqlcomm.ExecuteReader();

            try
            {
                _mySqlConnection.Open();
                MySqlDataReader myReader = sqlcomm.ExecuteReader();
                if (myReader.HasRows)
                {
                    Console.WriteLine("Your query generated results");
                    while (myReader.Read())
                    {
                        Console.WriteLine(myReader.GetString(0) + " - " + myReader.GetString(1));
                        ViewBag.id = (myReader.GetString(0));
                        ViewBag.value = (myReader.GetString(1));

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
                // Close the connection in a finally block to ensure it's closed even if an exception occurs
                _mySqlConnection.Close();
            }


            return View();
        }

        // GET: TemperatureController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: TemperatureController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TemperatureController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TemperatureController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TemperatureController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TemperatureController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TemperatureController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}