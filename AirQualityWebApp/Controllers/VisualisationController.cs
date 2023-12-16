using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;


namespace Xampp_Test2.Controllers
{
    public class VisualisationController : Controller
    {
        public IActionResult Histogram(string timestampYear, string timestampMonth, string timestampDay)
        {
            ViewBag.TimestampYear = timestampYear;
            ViewBag.TimestampMonth = timestampMonth;
            ViewBag.TimestampDay = timestampDay;
            string timestamp = "";

            // Construct timestamp string based on provided inputs
            if (!string.IsNullOrEmpty(timestampYear))
            {
                timestamp += timestampYear;
            }

            if (!string.IsNullOrEmpty(timestampMonth))
            {
                timestamp += $"-{timestampMonth.PadLeft(2, '0')}";
            }

            if (!string.IsNullOrEmpty(timestampDay))
            {
                timestamp += $"-{timestampDay.PadLeft(2, '0')}";
            }


            DateTime? filterTimestamp = null;
            if (DateTime.TryParseExact(timestamp, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedTimestamp))
            {
                filterTimestamp = parsedTimestamp;
            }
            // Try parsing with year and month format (yyyy-MM)
            else if (DateTime.TryParseExact(timestamp, "yyyy-MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedTimestamp))
            {
                filterTimestamp = parsedTimestamp;
            }
            else if (DateTime.TryParseExact(timestamp, "yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedTimestamp))
            {
                filterTimestamp = parsedTimestamp;
            }

            float[] arduinoArray;
            float[] apiArray;

            try
            {
                if (filterTimestamp == null)
                {

                    string arduinoSqlQuery = "select temp_value from tbl_temp WHERE temp_value IS NOT NULL";
                    List<float> arduinoData = SQLQuery("db_arduino", arduinoSqlQuery);

                    if (arduinoData != null && arduinoData.Count > 0)
                    {
                        arduinoArray = arduinoData.ToArray();
                    }
                    else
                    {
                        arduinoArray = null;
                    }

                    string apiSqlQuery = "SELECT temperature FROM air_api WHERE temperature IS NOT NULL";
                    List<float> apiData = SQLQuery("db_air_api", apiSqlQuery);



                    if (apiData != null && apiData.Count > 0)
                    {
                        apiArray = apiData.ToArray();
                    }
                    else
                    {
                        apiArray = null;
                    }


                    if (arduinoData?[0] == 999999 || apiArray?[0] == 999999)
                    {
                        return View("Error");
                    }



                }
                else
                {
                    // Adjust the SQL queries based on the available timestamp components
                    string arduinoSqlQuery = $"SELECT temp_value FROM tbl_temp WHERE YEAR(timestamp) = {filterTimestamp?.Year}";

                    if (timestampMonth != null)
                    {
                        arduinoSqlQuery += $" AND MONTH(timestamp) = {filterTimestamp?.Month}";

                        if (timestampDay != null)
                        {
                            arduinoSqlQuery += $" AND DAY(timestamp) = {filterTimestamp?.Day}";
                        }
                    }

                    List<float> arduinoData = SQLQuery("db_arduino", arduinoSqlQuery);
                    arduinoArray = arduinoData.ToArray();

                    string apiSqlQuery = $"SELECT temperature FROM air_api WHERE YEAR(date) = {filterTimestamp?.Year}";

                    if (timestampMonth != null)
                    {
                        apiSqlQuery += $" AND MONTH(date) = {filterTimestamp?.Month}";

                        if (timestampDay != null)
                        {
                            apiSqlQuery += $" AND DAY(date) = {filterTimestamp?.Day}";
                        }
                    }

                    List<float> apiData = SQLQuery("db_air_api", apiSqlQuery);
                    apiArray = apiData.ToArray();

                    if (arduinoData?[0] == null || apiArray?[0] == null)
                    {
                        return View("Error");
                    }
                    if (arduinoData?[0] == 999999 || apiArray?[0] == 999999)
                    {
                        return View("Error");
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                return View("Error");
            }



            return View(new { DataArray = arduinoArray, DataArray2 = apiArray });
        }



        
            public List<float> SQLQuery(string database, string query)
            {
                List<float> data = new List<float>();

                string connectionString = $"server=finalprojectx00192288.database.windows.net;database=FinalProject;uid=ageraghty;password=x00192288!;";

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    string sqlquery = query;

                    using (SqlCommand sqlCommand = new SqlCommand(sqlquery, sqlConnection))
                    {
                        sqlCommand.CommandTimeout = 60;

                        try
                        {
                            sqlConnection.Open();

                        using (SqlDataReader myReader = sqlCommand.ExecuteReader())
                        {
                            int columnIndex = 0; 

                            // Check if the reader has columns
                            if (myReader.FieldCount > columnIndex)
                            {
                                while (myReader.Read())
                                {
                                    if (!myReader.IsDBNull(columnIndex))
                                    {
                                        // Handle conversion for both System.Double and System.Decimal
                                        object rawValue = myReader.GetValue(columnIndex);

                                        if (rawValue is double doubleValue)
                                        {
                                            float value = Convert.ToSingle(doubleValue);
                                            data.Add(value);
                                        }
                                        else if (rawValue is decimal decimalValue)
                                        {
                                            float value = Convert.ToSingle(decimalValue);
                                            data.Add(value);
                                        }
                                        else if (rawValue is float floatValue)
                                        {
                                            // If the value is  a float, add it
                                            data.Add(floatValue);
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Unsupported data type: {rawValue.GetType().FullName}");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("No columns found in the result set.");
                            }
                        }
                    }
                        catch (Exception e)
                        {
                            data.Add(999999);
                            Console.WriteLine("Query error: " + e.Message);
                        }
                    }
                }
                return data;
            }
        }
    }













