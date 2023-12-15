using System;

namespace Xampp_Test2.Models
{
    public class TemperatureTable
    {
        public string Status { get; set; }
        public Data Data { get; set; }
    }

    public class Data
    {
        public int Aqi { get; set; }
        public City City { get; set; }
        public string DominantPollutant { get; set; }
        public Iaqi Iaqi { get; set; }
        public Time Time { get; set; }
    }

    public class City
    {
        public string Name { get; set; }
        // Other properties...
    }

    public class Iaqi
    {
        public Humidity H { get; set; }
        public NO2 No2 { get; set; }
        public O3 O3 { get; set; }
        // Other properties...
    }

    public class Humidity
    {
        public int V { get; set; }
    }

    public class NO2
    {
        public double V { get; set; }
    }

    public class O3
    {
        public double V { get; set; }
    }

    // Add other necessary classes...

    public class Time
    {
        public string S { get; set; }
        public string Tz { get; set; }
        public long V { get; set; }
        public string Iso { get; set; }
    }
}
