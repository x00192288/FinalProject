using Xampp_Test2.Models;
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
        SqlCommand CreateCommand();
    }

    public class SqlConnectionWrapper : IDbConnectionWrapper
    {
        private readonly SqlConnection _sqlConnection;

        public SqlConnectionWrapper(string connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
        }

        public void Open()
        {
            _sqlConnection.Open();
        }

        public void Close()
        {
            _sqlConnection.Close();
        }

        public SqlCommand CreateCommand() 
        {
            return _sqlConnection.CreateCommand();
        }

        public void Dispose()
        {
            _sqlConnection.Dispose();
        }
    }

    public class TemperatureController : Controller
    {
        private readonly IDbConnectionWrapper _sqlConnection;

        public TemperatureController([FromServices] IDbConnectionWrapper temperatureDbConnection)
        {
            _sqlConnection = temperatureDbConnection ?? throw new ArgumentNullException(nameof(temperatureDbConnection));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _sqlConnection?.Dispose();
            }

            base.Dispose(disposing);
        }


        public ActionResult Details()
        {
            List<Temperature> temps = new List<Temperature>();
           
            string sqlquery = "select * from tbl_temp";

            SqlCommand sqlcomm = _sqlConnection.CreateCommand();
            sqlcomm.CommandText = sqlquery;

            sqlcomm.CommandTimeout = 60;
            

            try
            {
                _sqlConnection.Open();
                SqlDataReader myReader = sqlcomm.ExecuteReader();
                if (myReader.HasRows)
                {
                    Console.WriteLine("Your query generated results");
                    while (myReader.Read())
                    {
                        Console.WriteLine(myReader.GetValue(0).ToString() + " - " + myReader.GetValue(1).ToString());

                        ViewBag.id = myReader.GetValue(0).ToString();

                        ViewBag.value = ((double)myReader.GetValue(1)).ToString("N2");

                        
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
                // Close the connection 
                _sqlConnection.Close();
            }


            return View();
        }

        
    }
}