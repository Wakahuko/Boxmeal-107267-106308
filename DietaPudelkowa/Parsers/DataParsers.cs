using CsvHelper;
using DietaPudelkowa.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DietaPudelkowa.Parsers
{
    public class DataParsers
    {
        private string _filePath = ConfigurationManager.AppSettings["filePath"]??"";
        private IEnumerable<Product> _productList;
        private readonly Random _random = new Random();

       public DataParsers(string path = "")
        {
            if (!String.IsNullOrEmpty(path))
                _filePath = path;

            if (!File.Exists(_filePath))
            {
                Console.WriteLine("Plik nie istnieje.");

                Console.ReadKey();
                Environment.Exit(0);
            }
            else
            {
                using (var reader = new StreamReader(_filePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    _productList = csv.GetRecords<Product>().ToList();
                }
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productList;
        }

        public void ShowAllProducts()
        {
            foreach(var item in _productList)
            {
                string wege = item.Wege ? "TAK" : "NIE";
                Console.WriteLine($"{item.Nazwa} ({item.Rodzaj}),{item.Cal} Cal,{item.Cena} PLN, WEGETARAŃSKIE: {wege}");
            }
        }

        public void GenerateDiet(float cal, bool wege)
        {
            List<DayOfWeek> workDays = new List<DayOfWeek>();
            workDays.Add(DayOfWeek.Monday);
            workDays.Add(DayOfWeek.Tuesday);
            workDays.Add(DayOfWeek.Wednesday);
            workDays.Add(DayOfWeek.Thursday);
            workDays.Add(DayOfWeek.Friday);


            foreach (var day in workDays)
            {
                Console.Write(day);

                Console.Write("\nŚniadanie (");
                var sniadanie = GenerateThreeProducts(cal, wege);
                Console.Write($"{sniadanie.Names}) {sniadanie.Cal} cal\n");

                Console.Write("Obiad (");
                var obiad = GenerateThreeProducts(cal, wege);
                Console.Write($"{obiad.Names}) {obiad.Cal} cal\n");

                Console.Write("Kolacja (");
                var kolacja = GenerateThreeProducts(cal, wege);
                Console.Write($"{kolacja.Names}) {kolacja.Cal} cal\n");
                Console.WriteLine($"Wartość Kaloryczna Ogółem: {sniadanie.Cal + obiad.Cal + kolacja.Cal}\n");

            }
        }

        public CalPriceModel GenerateThreeProducts(float cal, bool ifWege)
        {
            string result="";
            int sumCal = 0;
            int sumPrice = 0;
            List<Product> products = new List<Product>();

            if (ifWege == true)
                products = _productList.Where(p => p.Wege == ifWege).ToList();
            else
                products = _productList.ToList();

            while (true)
            {
                int index;

                index = new Random().Next(0,products.Where(p => p.Rodzaj == "Przystawka").Count());
                var productPrzystawka = products.Where(p => p.Rodzaj == "Przystawka").ElementAt(index);
                result += productPrzystawka.Nazwa+"/";
                sumCal += productPrzystawka.Cal;
                sumPrice += productPrzystawka.Cena;
                //Thread.Sleep(10);

                index = _random.Next(products.Where(p => p.Rodzaj == "Główne").Count());
                var productGlowne = products.Where(p => p.Rodzaj == "Główne").ElementAt(index);
                result += productGlowne.Nazwa+"/";
                sumCal += productGlowne.Cal;
                sumPrice += productGlowne.Cena;
                //Thread.Sleep(10);

                index = _random.Next(products.Where(p => p.Rodzaj == "Deser").Count());
                var productDeser = products.Where(p => p.Rodzaj == "Deser").ElementAt(index);
                result += productDeser.Nazwa;
                sumCal += productDeser.Cal;
                sumPrice += productDeser.Cena;
                //Thread.Sleep(10);

                if (sumCal <= (cal + cal * 0.1) && sumCal >= (cal - cal * 0.1))
                    break;
                else
                {
                    sumCal = 0;
                    result = "";
                }
            }

            return new CalPriceModel() { Cal = sumCal, Price = sumPrice, Names = result };
        }


    }
}
