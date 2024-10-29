using ExoplanetExploration;
using System;
using Postcode6Importer;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleAppCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //foreach (var item in ExoplanetExploration.exoplanetCollection)
            //{
            //    Console.WriteLine($"Name {item.Name}  Mass {item.Mass}    Orbital{item.OrbitalPeriod}   Radius {item.Radius}");
            //}


            //foreach (var item in CcbImporter.observationsCollection)
            //{
            //    Console.WriteLine($"Measure {item.Measure}  PostCode6 {item.PostCode6}    Typegebruik {item.Typegebruik}   Value {item.Value}");
            //}

            var csv = new CcbImporter.Postcode6("mongodb://outputteam:Outputteam2018!@localhost:27017", "", "O-900001NED-201809120900");
            csv.ImportCsv();

            var a = new Class1();
            Console.WriteLine("Hello World from C#!" + a.X);
            Console.ReadKey();
        }
    }
}
